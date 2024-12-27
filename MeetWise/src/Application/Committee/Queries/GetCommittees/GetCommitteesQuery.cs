using MediatR;
using MeetWise.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MeetWise.Application
{
    public class GetCommitteesQuery : IRequest<List<CommitteeQuerieDto>> { }

    public class GetCommitteesQueryHandler : IRequestHandler<GetCommitteesQuery, List<CommitteeQuerieDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCommitteesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommitteeQuerieDto>> Handle(GetCommitteesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Committees
                .Select(c => new CommitteeQuerieDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Details = c.Details,
                    MemberCount = c.Members.Count
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class CommitteeQuerieDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Details { get; set; }
        public int MemberCount { get; set; }
    }
}
