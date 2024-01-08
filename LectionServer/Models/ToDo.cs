namespace LectionServer.Models
{
    public class ToDo
    {
        public required Guid Id { get; init; }
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public required bool IsCompleted { get; set; } = false;
        public required string Lesson { get; set; } = "";
        public required string Data { get; set; } = "";
    }
}
