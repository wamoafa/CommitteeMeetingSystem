using MeetWise.Application.DTOs;
using MeetWise.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeetWise.Web.Endpoints
{
    [Route("api/sessions")]
    [ApiController]
    public class SessionEndpoints : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionEndpoints(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto, CancellationToken cancellationToken)
        {
            var id = await _sessionService.CreateSessionAsync(sessionDto, cancellationToken);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSession([FromBody] SessionDto sessionDto, CancellationToken cancellationToken)
        {
            await _sessionService.UpdateSessionAsync(sessionDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id, CancellationToken cancellationToken)
        {
            await _sessionService.DeleteSessionAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id, CancellationToken cancellationToken)
        {
            var session = await _sessionService.GetSessionByIdAsync(id, cancellationToken);
            if (session == null)
                return NotFound();
            return Ok(session);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSessions(CancellationToken cancellationToken)
        {
            var sessions = await _sessionService.GetAllSessionsAsync(cancellationToken);
            return Ok(sessions);
        }
    }
}
