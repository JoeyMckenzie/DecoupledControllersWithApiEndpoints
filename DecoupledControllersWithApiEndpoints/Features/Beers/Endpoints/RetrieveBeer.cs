using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using DecoupledControllersWithApiEndpoints.Data;
using DecoupledControllersWithApiEndpoints.Features.Beers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace DecoupledControllersWithApiEndpoints.Features.Beers.Endpoints
{
    [Route(Routes.BeerUri)]
    public class RetrieveBeer : BaseAsyncEndpoint<int, Beer>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RetrieveBeer> _logger;

        public RetrieveBeer(ApplicationDbContext context, ILogger<RetrieveBeer> logger) =>
            (_context, _logger) = (context, logger);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Beer), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieve a beer",
            Description = "Retrieves a beer given a valid ID",
            OperationId = nameof(RetrieveBeer),
            Tags = new[] { nameof(RetrieveBeer) }
        )]
        public async override Task<ActionResult<Beer>> HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Received request to retrieve beer with ID {id}");

            var beer = await _context.Beers.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if (beer is null)
            {
                return NotFound($"Beer with ID {id} was not found");
            }
            
            return Ok(beer);
        }
    }
}
