using MeetWise.Domain.Entities;
using MediatR;

namespace MeetWise.Domain.Events
{
    public class DeleteCommitteeEvent : INotification
    {
        public Committee Committee { get; }

        public DeleteCommitteeEvent(Committee committee)
        {
            Committee = committee;
        }
    }
}
