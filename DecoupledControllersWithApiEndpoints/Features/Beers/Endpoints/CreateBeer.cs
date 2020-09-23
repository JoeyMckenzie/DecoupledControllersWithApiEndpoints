using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using DecoupledControllersWithApiEndpoints.Data;
using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace DecoupledControllersWithApiEndpoints.Features.Beers.Endpoints
{
    [Route(Routes.BeerUri)]
    public class CreateBeer : BaseAsyncEndpoint<CreateBeerDto, Beer>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateBeer> _logger;

        public CreateBeer(ApplicationDbContext dbContext, ILogger<CreateBeer> logger) =>
            (_context, _logger) = (dbContext, logger);

        [HttpPost]
        [ProducesResponseType(typeof(Beer), StatusCodes.Status201Created)]
        [SwaggerOperation(
            Summary = "Creates a beers",
            Description = "Creates a beer in the database using Entity Framework Core",
            OperationId = nameof(CreateBeer),
            Tags = new[] { nameof(CreateBeer) }
        )]
        public override async Task<ActionResult<Beer>> HandleAsync(CreateBeerDto request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Received request to create beer...");

            // Grab a reference to a new Beer entity instance
            var beerToAdd = new Beer
            {
                Name = request.Name,
                Style = Enum.TryParse(request.Style, true, out BeerStyle style) ? style : BeerStyle.Other,
                Ibu = request.Ibu,
                Abv = request.Abv,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Add the beer as a tracked entity and grab a reference to the managed entity returned to us
            var beer = await _context.AddAsync(beerToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Created(new Uri($"/{Routes.BeerUri}/{beer.Entity.Id}", UriKind.Relative), beer.Entity);
        }
    }
}
