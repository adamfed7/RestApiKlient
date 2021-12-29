using Microsoft.EntityFrameworkCore;
using RestApi2.Models;


namespace klient.Models
{
    public class FakeContext : DbContext
    {
        public FakeContext(DbContextOptions<FakeContext> options)
            : base(options)
        {
        }

        public DbSet<RestItem> TodoItems { get; set; }

        public DbSet<RestApi2.Models.RestItem> TodoItem { get; set; }
    }

}
