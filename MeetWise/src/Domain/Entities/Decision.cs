namespace MeetWise.Domain.Entities
{
    public class Decision : BaseAuditableEntity
    {
        public string Content { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
