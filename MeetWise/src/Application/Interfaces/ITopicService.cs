using MeetWise.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeetWise.Application.Interfaces
{
    public interface ITopicService
    {
        Task<int> CreateTopicAsync(TopicDto topicDto, CancellationToken cancellationToken);
        Task UpdateTopicAsync(TopicDto topicDto, CancellationToken cancellationToken);
        Task DeleteTopicAsync(int topicId, CancellationToken cancellationToken);
        Task<TopicDto?> GetTopicByIdAsync(int topicId, CancellationToken cancellationToken);
        Task<IEnumerable<TopicDto>> GetAllTopicsAsync(CancellationToken cancellationToken);
    }
}
