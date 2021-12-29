using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //IConfiguration
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestApi2.Models;
using Microsoft.AspNetCore.Authorization;


namespace klient.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly HttpClient client;
        private readonly string WebApiPath;
        private readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiPath = _configuration["RestApi2Config:Url"];   //read from appsettings.json
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["RestApi2Config:ApiKey"]);   //use on any http calls      
        }


        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            List<RestItem> rests = null;
            HttpResponseMessage response = await client.GetAsync(WebApiPath);
            if (response.IsSuccessStatusCode)
            {
                rests = await response.Content.ReadAsAsync<List<RestItem>>();  //requires System.Net.Http.Formatting.Extension
            }
            return View(rests);
        }



        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                RestItem rest = await response.Content.ReadAsAsync<RestItem>();
                return View(rest);
            }
            return NotFound();
        }


        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  //rest add deoc to slides
        public async Task<ActionResult> Create([Bind("Id,Nazwa_produktu,Ilosc,Zakupiony")] RestItem rest)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(WebApiPath, rest);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(rest);
        }



        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                RestItem rest = await response.Content.ReadAsAsync<RestItem>();
                return View(rest);
            }
            return NotFound();
        }


        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Nazwa_produktu,Ilosc,Zakupiony")] RestItem rest)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(WebApiPath + id, rest);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(rest);
        }




        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                RestItem rest = await response.Content.ReadAsAsync<RestItem>();
                return View(rest);
            }
            return NotFound();
        }


        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, int notUsed = 0)
        {
            HttpResponseMessage response = await client.DeleteAsync(WebApiPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}


