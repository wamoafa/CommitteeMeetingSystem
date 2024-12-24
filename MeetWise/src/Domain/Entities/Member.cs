namespace MeetWise.Domain.Entities
{
    public class Member : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Committee> Committees { get; set; } = new List<Committee>();
    }
}
