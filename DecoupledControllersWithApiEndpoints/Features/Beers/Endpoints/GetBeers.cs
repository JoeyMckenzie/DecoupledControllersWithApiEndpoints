using Ardalis.ApiEndpoints;
using DecoupledControllersWithApiEndpoints.Data;
using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DecoupledControllersWithApiEndpoints.Features.Beers.Endpoints
{
    [Route(Routes.BaseUri)]
    public class GetBeers : BaseAsyncEndpoint<IEnumerable<Beer>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetBeers> _logger;

        public GetBeers(ApplicationDbContext context, ILogger<GetBeers> logger) =>
            (_context, _logger) = (context, logger);

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Beer>), 200)]
        [SwaggerOperation(
            Summary = "Retrieves a list of beers",
            Description = "Retrieves a list of beers from the database",
            OperationId = nameof(GetBeers),
            Tags = new[] { nameof(GetBeers) }
        )]
        public override async Task<ActionResult<IEnumerable<Beer>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Received request to retrieve all beers...");
            return Ok(await _context.Beers.ToListAsync(cancellationToken));
        }
    }
}
