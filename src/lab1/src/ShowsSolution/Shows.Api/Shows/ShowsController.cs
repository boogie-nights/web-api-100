using Marten;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Shows.Entities;
using Shows.Api.Shows.Models;

namespace Shows.Api.Shows;

public class ShowsController(IDocumentSession session) : ControllerBase
{

    [HttpGet("api/shows")]
    public async Task<ActionResult> GetAllShowsAsync()
    {
        var shows = await session.Query<ShowDetailsEntity>()
            .OrderByDescending(show => show.CreatedAt)
            .ToListAsync();

        return Ok(shows);
    }

    [HttpGet("api/shows/{id:guid}")]
    public async Task<ActionResult> GetShowByIdAsync(Guid id)
    {
        // Grab the show from the DB
        var savedShow = await session.Query<ShowDetailsEntity>()
            .Where(show => show.Id == id)
            .SingleOrDefaultAsync();

        if (savedShow == null)
        {
            return NotFound();
        }
        else
        {
            var response = new ShowDetailsModel
            {
                Id = savedShow.Id,
                CreatedAt = savedShow.CreatedAt,
                Name = savedShow.Name,
                Description = savedShow.Description,
                StreamingService = savedShow.StreamingService
            };
            return Ok(response);
        }
    }

    [HttpPost("api/shows")]
    public async Task<ActionResult> AddShowAsync(
        [FromBody] ShowCreateModel showCreateModel
        )
    {
        // Create the entity to store in the DB from incoming data
        var showCreateEntity = new ShowDetailsEntity
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = showCreateModel.Name,
            Description = showCreateModel.Description,
            StreamingService = showCreateModel.StreamingService
        };

        // Store the Entity in the DB
        session.Store(showCreateEntity);
        await session.SaveChangesAsync();

        // Build a response object from the data in the created DB entity
        var response = new ShowDetailsModel
        {
            Id = showCreateEntity.Id,
            CreatedAt = showCreateEntity.CreatedAt,
            Name = showCreateEntity.Name,
            Description = showCreateEntity.Description,
            StreamingService = showCreateEntity.StreamingService
        };

        return StatusCode(201, response);
    }
}