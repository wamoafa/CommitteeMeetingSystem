using MeetWise.Application.DTOs;
using MeetWise.Application.Interfaces;
using MeetWise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MeetWise.Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly IApplicationDbContext _context;

        public TopicService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTopicAsync(TopicDto topicDto, CancellationToken cancellationToken)
        {
           
            var topic = new Topic
            {
                Title = topicDto.Title,
                Description = topicDto.Description,
                SessionId = topicDto.SessionId,
                IsActive = topicDto.IsActive,
                IsDeleted = topicDto.IsDeleted
            };

            _context.Topics.Add(topic);
            await _context.SaveChangesAsync(cancellationToken);
            return topic.Id;
        }

        public async Task UpdateTopicAsync(TopicDto topicDto, CancellationToken cancellationToken)
        {
            
            var topic = await _context.Topics.FindAsync(new object[] { topicDto.Id }, cancellationToken);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with Id {topicDto.Id} not found.");

           
            topic.Title = topicDto.Title;
            topic.Description = topicDto.Description;
            topic.SessionId = topicDto.SessionId;
            topic.IsActive = topicDto.IsActive;
            topic.IsDeleted = topicDto.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            
            var topic = await _context.Topics.FindAsync(new object[] { topicId }, cancellationToken);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with Id {topicId} not found.");

           
            topic.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TopicDto?> GetTopicByIdAsync(int topicId, CancellationToken cancellationToken)
        {
           
            return await _context.Topics
                .AsNoTracking()
                .Where(t => t.Id == topicId && !t.IsDeleted)
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    SessionId = t.SessionId,
                    IsActive = t.IsActive,
                    IsDeleted = t.IsDeleted
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsAsync(CancellationToken cancellationToken)
        {
           
            return await _context.Topics
                .AsNoTracking()
                .Where(t => !t.IsDeleted)
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    SessionId = t.SessionId,
                    IsActive = t.IsActive,
                    IsDeleted = t.IsDeleted
                })
                .ToListAsync(cancellationToken);
        }
    }
}
