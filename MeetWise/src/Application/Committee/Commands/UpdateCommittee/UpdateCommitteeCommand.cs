using MediatR;
using MeetWise.Domain.Entities;

namespace MeetWise.Application
{
    public class UpdateCommitteeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Details { get; set; }
    }

    public class UpdateCommitteeCommandHandler : IRequestHandler<UpdateCommitteeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCommitteeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCommitteeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Committees.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Committee), request.Id.ToString());
            }

            entity.Name = request.Name;
            entity.Details = request.Details;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
