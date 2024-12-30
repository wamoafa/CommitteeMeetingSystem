using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetWise.Application
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int SessionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}

