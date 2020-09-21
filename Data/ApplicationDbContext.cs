using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoupledControllersWithApiEndpoints.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
