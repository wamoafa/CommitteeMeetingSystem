using MeetWise.Domain.Entities;
using MediatR;

namespace MeetWise.Domain.Events
{
    public class UpdateCommitteeEvent : INotification
    {
        public Committee Committee { get; }

        public UpdateCommitteeEvent(Committee committee)
        {
            Committee = committee;
        }
    }
}
