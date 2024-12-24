using MediatR;
using Microsoft.Extensions.Logging;

namespace MeetWise.Application.Committee.EventHandlers
{
    public class CreateCommitteeEventHandler : INotificationHandler<CreateCommitteeEvent>
    {
        private readonly ILogger<CreateCommitteeEventHandler> _logger;

        public CreateCommitteeEventHandler(ILogger<CreateCommitteeEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreateCommitteeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MeetWise Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
