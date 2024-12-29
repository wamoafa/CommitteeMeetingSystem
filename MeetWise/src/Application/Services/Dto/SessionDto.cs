namespace MeetWise.Application.DTOs
{
    public class SessionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Details { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CommitteeId { get; set; }
    }
}
