using MediatR;

namespace MeetWise.Application.Committee.Commands
{
    public class CreateCommitteeCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
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
            var entity = new Committee
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Committees.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
