using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using RestApi2.Models;
using RestApi2.Attributes;

namespace RestApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class RestItemsController : ControllerBase
    {
        private readonly RestContext _context;

        public RestItemsController(RestContext context)
        {
            _context = context;
        }

        // GET: api/RestItems   notice: we don't use method name
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Zwraca wszystkie produkty.", "Używa EF")]
        [SwaggerResponse(200, "Sukces", Type = typeof(List<RestItem>))]
        public async Task<ActionResult<IEnumerable<RestItem>>> GetRestItems()
        {
            return await _context.RestItems.ToListAsync();  //http 200
        }



        // GET: api/RestApiItems/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Zwraca produkt o podanym {id}.", "Używa EF FindAsync()")]
        [SwaggerResponse(200, "Znaleziono produkt o podanym {id}", Type = typeof(RestItem))]
        [SwaggerResponse(404, "Nie znaleziono produktu o podanym {id}")]
        public async Task<ActionResult<RestItem>> GetRestItem(
            [SwaggerParameter("Podaj nr produktu które chcesz odczytać", Required = true)]
            int id)
        {
            var restItem = await _context.RestItems.FindAsync(id);

            if (restItem == null)
            {
                return NotFound(); //http 404
            }

            return restItem;    //http 200
        }


        // PUT: api/RestApiItems/5        
        [HttpPut("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Aktualizuje produkt o podanym {id}.", "Używa EF")]
        [SwaggerResponse(204, "Zaktualizowano produkt o podanym {id}")]
        [SwaggerResponse(400, "Nie rozpoznano danych wejściowych")]
        [SwaggerResponse(404, "Nie znaleziono produktu o podanym {id}")]
        public async Task<IActionResult> PutRestItem(
            [SwaggerParameter("Podaj nr produktu które chcesz zaktualizować", Required = true)]
            int id,
            [SwaggerParameter("Definicja produktu", Required = true)]
            RestItem restItem)
        {
            if (id != restItem.Id)
            {
                return BadRequest(); //http 400
            }

            _context.Entry(restItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestItemExists(id))
                {
                    return NotFound();  //http 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //http 204
        }


        // POST: api/RestApiItems        
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation("Tworzy nowy produkt.", "Używa EF")]
        [SwaggerResponse(201, "Zapis z sukcesem.", Type = typeof(RestItem))]
        public async Task<ActionResult<RestItem>> PostRestItem(
            [SwaggerParameter("Definicja produktu", Required = true)]
            RestItem restItem)
        {
            _context.RestItems.Add(restItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestItem", new { id = restItem.Id }, restItem);  //http 201, add Location header
        }



        // DELETE: api/RestApiItems/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Usuwa produkt o podanym {id}.", "Używa EF")]
        [SwaggerResponse(204, "Usunięto produkt o podanym {id}")]
        [SwaggerResponse(404, "Nie znaleziono produktu o podanym {id}")]
        public async Task<IActionResult> DeleteRestItem(
            [SwaggerParameter("Podaj nr produktu które chcesz usunąć", Required = true)]
            int id)
        {
            var restItem = await _context.RestItems.FindAsync(id);
            if (restItem == null)
            {
                return NotFound();  //http 404
            }

            _context.RestItems.Remove(restItem);
            await _context.SaveChangesAsync();

            return NoContent(); //http 204
        }



        private bool RestItemExists(int id)
        {
            return _context.RestItems.Any(e => e.Id == id);
        }
    }
}
