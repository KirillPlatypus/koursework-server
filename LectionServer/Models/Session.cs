namespace LectionServer.Models
{
    public class Session
    {
        public required Guid Id { get; init; }
        public required Guid UserId { get; set; }
        public required string DataStartSemester { get; set; }
        public required string DataEndSemester { get; set; }
        public required string DataStartSession { get; set; }
    }
}
