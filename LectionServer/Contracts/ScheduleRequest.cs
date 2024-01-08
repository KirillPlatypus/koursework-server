namespace LectionServer.Contracts
{
    public record ScheduleRequest
    {
        public required string Name { get; init; }
        public required string Type { get; set; }
        public required string Time { get; init; }
        public required string Place { get; init; }
        public required string Teacher { get; init; }
        public required string Week { get; init; }
        public required string Data { get; init; }
    }
}
