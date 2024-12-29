namespace MeetWise.Domain.Entities
{
    public class Committee : BaseAuditableEntity
    {
        public string? Name { get; set; } = null!;
        public string? Details { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();

    }
}
