namespace Shows.Api.Shows.Models;

// Data returned to user, built from show details entity
public record ShowDetailsModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}