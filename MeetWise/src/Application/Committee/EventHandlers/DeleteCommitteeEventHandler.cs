using MediatR;
using Microsoft.Extensions.Logging;

namespace MeetWise.Application.Committee.EventHandlers
{
    public class DeleteCommitteeEventHandler : INotificationHandler<DeleteCommitteeEvent>
    {
        private readonly ILogger<DeleteCommitteeEventHandler> _logger;

        public DeleteCommitteeEventHandler(ILogger<DeleteCommitteeEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeleteCommitteeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MeetWise Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
