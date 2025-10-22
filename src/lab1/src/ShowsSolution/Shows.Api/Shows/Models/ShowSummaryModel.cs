namespace Shows.Api.Shows.Models;

public record ShowSummaryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}