using MediatR;
using MeetWise.Domain.Events;
using Microsoft.Extensions.Logging;

namespace MeetWise.Application
{
    public class UpdateCommitteeEventHandler : INotificationHandler<UpdateCommitteeEvent>
    {
        private readonly ILogger<UpdateCommitteeEventHandler> _logger;

        public UpdateCommitteeEventHandler(ILogger<UpdateCommitteeEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UpdateCommitteeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MeetWise Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
