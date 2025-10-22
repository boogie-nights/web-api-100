using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Shows.Api.Shows;

public class ShowsController(IDocumentSession session) : ControllerBase
{


    [HttpGet("api/shows")]
    public async Task<ActionResult> GetAllShowsAsync()
    {
        var shows = await session.Query<ShowDetailsEntity>()
            .OrderBy(show => show.CreatedAt)
            .ToListAsync();

        //var response = new CollectionResponseModel<ShowSummaryModel>();

        //response.Data = shows.Select(show => new ShowSummaryModel
        //{
        //    Id = show.Id,
        //    Name = show.Name,
        //}).ToList();

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

public class CollectionResponseModel<T>
{
    public IList<T> Data { get; set; } = new List<T>();
}

public record ShowSummaryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// Data returned to user, built from show details entity
public record ShowDetailsModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}

// Data Received for Post
public record ShowCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}

// Data stored in the DB
public record ShowDetailsEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}