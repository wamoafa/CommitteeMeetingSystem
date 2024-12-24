namespace MeetWise.Domain.Entities
{
    public class Session : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Details { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public ICollection<Decision> Decisions { get; set; } = new List<Decision>();
    }
}
