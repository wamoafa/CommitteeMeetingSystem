using MediatR;
using MeetWise.Application.Common.Interfaces;
using MeetWise.Domain.Entities;

public class CreateCommitteeCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Details { get; set; }
    public List<int>? MemberIds { get; set; }
}

public class CreateCommitteeCommandHandler : IRequestHandler<CreateCommitteeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCommitteeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCommitteeCommand request, CancellationToken cancellationToken)
    {
        var committee = new Committee
        {
            Name = request.Name,
            Details = request.Details,
        };

        if (request.MemberIds != null && request.MemberIds.Count > 0)
        {
            committee.Members = _context.Members
                .Where(m => request.MemberIds.Contains(m.Id))
                .ToList();
        }

        _context.Committees.Add(committee);
        await _context.SaveChangesAsync(cancellationToken);

        return committee.Id;
    }
}
