using MediatR;

namespace MeetWise.Application.Committee.Commands
{
    public class UpdateCommitteeCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdateCommitteeCommandHandler : IRequestHandler<UpdateCommitteeCommand>
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
                throw new NotFoundException(nameof(Committee), request.Id);
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
