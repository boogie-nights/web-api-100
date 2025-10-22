namespace Shows.Api.Shows.Entities;

// Data stored in the DB
public record ShowDetailsEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}
