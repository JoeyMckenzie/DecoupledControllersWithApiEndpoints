using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using DecoupledControllersWithApiEndpoints.Data;
using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DecoupledControllersWithApiEndpoints.Features.Beers.Endpoints
{
    [Route("api/beers")]
    public class CreateBeer : BaseAsyncEndpoint<CreateBeerDto, Beer>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CreateBeer> _logger;

        public CreateBeer(ApplicationDbContext dbContext, ILogger<CreateBeer> logger) =>
            (_dbContext, _logger) = (dbContext, logger);

        [HttpPost]
        public override async Task<ActionResult<Beer>> HandleAsync(CreateBeerDto request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Received request to create beer...");

            var beerToAdd = new Beer
            {
                Name = request.Name,
                Style = request.Style,
                Ibu = request.Ibu,
                Abv = request.Abv,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var beer = await _dbContext.AddAsync(beerToAdd, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Created(new Uri($"/api/beers/{beer.Entity.Id}", UriKind.Relative), beer.Entity);
        }
    }
}
