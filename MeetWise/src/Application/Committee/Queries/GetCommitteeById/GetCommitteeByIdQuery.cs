using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MeetWise.Application.Common.Interfaces;
using MeetWise.Domain.Entities;

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
        var committee = await _context.Committees
            .FindAsync(request.Id);

        if (committee == null || committee.IsDeleted)
        {
            throw new NotFoundException(nameof(Committee), request.Id);
        }

        return new CommitteeDto
        {
            Id = committee.Id,
            Name = committee.Name,
            Details = committee.Details
        };
    }
}

public class CommitteeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
}
