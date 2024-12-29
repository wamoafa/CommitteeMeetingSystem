namespace MeetWise.Domain.Entities
{
    public class Session : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Details { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public Committee Committee { get; set; } = null!; // كائن اللجنة المرتبط
        public ICollection<Member> Members { get; set; } = new List<Member>(); // إضافة العلاقة مع الأعضاء
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public ICollection<Decision> Decisions { get; set; } = new List<Decision>();
    }
}
