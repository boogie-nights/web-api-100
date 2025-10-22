namespace Shows.Api.Shows.Models;

// Data Received for Post
public record ShowCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}