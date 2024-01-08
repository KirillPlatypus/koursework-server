namespace LectionServer.Models;

public record Book
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; set; }
    public required string Author { get; set; }
    public required string Name { get; set; }
}