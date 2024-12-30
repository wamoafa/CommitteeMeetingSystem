using MeetWise.Application;
using MeetWise.Application.DTOs;
using MeetWise.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeetWise.Web.Endpoints
{
    [Route("api/topics")]
    [ApiController]
    public class TopicEndpoints : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicEndpoints(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDto topicDto, CancellationToken cancellationToken)
        {
            var id = await _topicService.CreateTopicAsync(topicDto, cancellationToken);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTopic([FromBody] TopicDto topicDto, CancellationToken cancellationToken)
        {
            await _topicService.UpdateTopicAsync(topicDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id, CancellationToken cancellationToken)
        {
            await _topicService.DeleteTopicAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById(int id, CancellationToken cancellationToken)
        {
            var topic = await _topicService.GetTopicByIdAsync(id, cancellationToken);
            if (topic == null)
                return NotFound();
            return Ok(topic);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics(CancellationToken cancellationToken)
        {
            var topics = await _topicService.GetAllTopicsAsync(cancellationToken);
            return Ok(topics);
        }
    }
}
