namespace LectionServer.Contracts
{
    public record SessionRequest
    {
        public required string DataStartSemester { get; init; }
        public required string DataEndSemester { get; init; }
        public required string DataStartSession { get; init; }
    }
}
