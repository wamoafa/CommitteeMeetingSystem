using MediatR;
using MeetWise.Application.Common.Interfaces;
using System.Collections.Generic;

public class GetCommitteesQuery : IRequest<List<CommitteeDto>> { }

public class GetCommitteesQueryHandler : IRequestHandler<GetCommitteesQuery, List<CommitteeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCommitteesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommitteeDto>> Handle(GetCommitteesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Committees
            .Select(c => new CommitteeDto
            {
                Id = c.Id,
                Name = c.Name,
                Details = c.Details,
                MemberCount = c.Members.Count
            })
            .ToListAsync(cancellationToken);
    }
}

public class CommitteeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public int MemberCount { get; set; }
}
