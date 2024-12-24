using MediatR;

namespace MeetWise.Application.Committee.Queries
{
    public class GetAllCommitteesQuery : IRequest<List<CommitteeDto>>
    {
    }

    public class GetAllCommitteesQueryHandler : IRequestHandler<GetAllCommitteesQuery, List<CommitteeDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCommitteesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommitteeDto>> Handle(GetAllCommitteesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Committees
                .Select(c => new CommitteeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync(cancellationToken);
        }
    }
}
