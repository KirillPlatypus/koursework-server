namespace LectionServer.Models
{
    public class Schedule
    {
        public required Guid Id { get; init; }
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Time { get; set; }
        public required string Place { get; set; }
        public required string Teacher { get; set; }
        public required string Week { get; set; }
        public required string Data { get; set; }//либо день недели 0-6 либо дата
    }
}
