namespace MeetWise.Domain.Entities
{
    public class Topic : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
