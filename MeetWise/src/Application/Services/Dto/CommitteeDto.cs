using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetWise.Application.Services;
public class CommitteeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Details { get; set; }
    public bool? IsActive { get; set; }
    public List<MemberDto>? Members { get; set; }
}
public class MemberDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? NationalId { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
