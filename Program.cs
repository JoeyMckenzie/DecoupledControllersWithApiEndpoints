using System;
using System.Threading;
using DecoupledControllersWithApiEndpoints.Data;
using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DecoupledControllersWithApiEndpoints
{
    public class Program
    {
        private static void SeedDatabase(ApplicationDbContext dbContext)
        {
            var beersToAdd = new Beer[]
            {
                new Beer
                {
                    Name = "Hexagenia",
                    Abv = 7.4m,
                    Ibu = 120,
                    Style = BeerStyle.Ipa,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Beer
                {
                    Name = "Hazy Little",
                    Abv = 6.8m,
                    Ibu = 90,
                    Style = BeerStyle.Hazy,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Beer
                {
                    Name = "Scrimshaw",
                    Abv = 5.4m,
                    Ibu = 20,
                    Style = BeerStyle.Lager,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            dbContext.AddRange(beersToAdd);
            dbContext.SaveChanges();
        }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var serviceContainer = host.Services.CreateScope();
            var dbContext = serviceContainer.ServiceProvider.GetService<ApplicationDbContext>();

            SeedDatabase(dbContext);
            serviceContainer.Dispose();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
