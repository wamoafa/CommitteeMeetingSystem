using MediatR;

namespace MeetWise.Application.Committee.Commands
{
    public class DeleteCommitteeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCommitteeCommandHandler : IRequestHandler<DeleteCommitteeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCommitteeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCommitteeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Committees.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Committee), request.Id);
            }

            _context.Committees.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
