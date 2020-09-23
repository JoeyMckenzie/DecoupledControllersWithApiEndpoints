using Ardalis.ApiEndpoints;
using DecoupledControllersWithApiEndpoints.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DecoupledControllersWithApiEndpoints.Features.Beers.Endpoints
{
    [Route(Routes.BeerUri)]
    public class DeleteBeer : BaseAsyncEndpoint<int, NoContentResult>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeleteBeer> _logger;

        public DeleteBeer(ApplicationDbContext context, ILogger<DeleteBeer> logger) =>
            (_context, _logger) = (context, logger);

        [HttpDelete("{id}")]
        public override async Task<ActionResult<NoContentResult>> HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Received request to delete beer with ID {id}...");

            // Grab a reference to the beer to delete from the database
            var beer = await _context.Beers.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            
            // Invalidate the request if no beer is found
            if (beer is null)
            {
                return NotFound($"Beer with ID {id} was not found");
            }

            // Remove the beer from tracked state and update the database
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
