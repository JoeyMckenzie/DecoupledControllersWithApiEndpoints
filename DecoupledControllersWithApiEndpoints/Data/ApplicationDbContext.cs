using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DecoupledControllersWithApiEndpoints.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Beer>()
                .Property(b => b.Style)
                .HasConversion(new EnumToStringConverter<BeerStyle>());
        }
    }
}
