using Microsoft.EntityFrameworkCore;

namespace RestApi2.Models
{
    public class RestContext : DbContext
    {
        public RestContext(DbContextOptions<RestContext> options)
            : base(options)
        {
        }

        public DbSet<RestApi2.Models.RestItem> RestItems { get; set; }
    }
}