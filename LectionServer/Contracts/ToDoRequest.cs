namespace LectionServer.Contracts
{
    public record ToDoRequest
    {
        public required string Name { get; init; }
        public required bool IsCompleted { get; set; } = false;
        public required string Lesson { get; init; } = "";
        public required string Data { get; init; } = "";
    }
}
