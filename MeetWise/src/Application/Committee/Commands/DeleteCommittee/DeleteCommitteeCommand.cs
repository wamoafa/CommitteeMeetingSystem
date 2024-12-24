using MediatR;
using MeetWise.Application.Common.Interfaces;
using MeetWise.Domain.Entities;

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
        var committee = await _context.Committees.FindAsync(request.Id);
        if (committee == null)
            throw new NotFoundException(nameof(Committee), request.Id);

        committee.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
