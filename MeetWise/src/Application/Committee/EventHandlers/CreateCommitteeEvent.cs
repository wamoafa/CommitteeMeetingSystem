using MeetWise.Domain.Entities;
using MediatR;

namespace MeetWise.Domain.Events
{
    public class CreateCommitteeEvent : INotification
    {
        public Committee Committee { get; }

        public CreateCommitteeEvent(Committee committee)
        {
            Committee = committee;
        }
    }
}
