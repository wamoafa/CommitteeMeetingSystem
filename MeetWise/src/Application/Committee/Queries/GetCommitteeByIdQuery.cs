using MediatR;

namespace MeetWise.Application.Committee.Queries
{
    public class GetCommitteeByIdQuery : IRequest<CommitteeDto>
    {
        public int Id { get; set; }
    }

    public class GetCommitteeByIdQueryHandler : IRequestHandler<GetCommitteeByIdQuery, CommitteeDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCommitteeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommitteeDto> Handle(GetCommitteeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Committees
                .Where(c => c.Id == request.Id)
                .Select(c => new CommitteeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Committee), request.Id);
            }

            return entity;
        }
    }
}
