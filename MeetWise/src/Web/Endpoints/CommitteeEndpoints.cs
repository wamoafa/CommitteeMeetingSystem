using MeetWise.Application.Interfaces;
using MeetWise.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeetWise.Web.Endpoints
{
    [Route("api/committees")]
    [ApiController]
    public class CommitteeEndpoints : ControllerBase
    {
        private readonly ICommitteeService _committeeService;

        public CommitteeEndpoints(ICommitteeService committeeService)
        {
            _committeeService = committeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommittee([FromBody] Committee committee, CancellationToken cancellationToken)
        {
            var id = await _committeeService.CreateCommitteeAsync(committee, cancellationToken);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCommittee([FromBody] Committee committee, CancellationToken cancellationToken)
        {
            await _committeeService.UpdateCommitteeAsync(committee, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommittee(int id, CancellationToken cancellationToken)
        {
            await _committeeService.DeleteCommitteeAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommitteeById(int id, CancellationToken cancellationToken)
        {
            var committee = await _committeeService.GetCommitteeByIdAsync(id, cancellationToken);
            return Ok(committee);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommittees(CancellationToken cancellationToken)
        {
            var committees = await _committeeService.GetAllCommitteesAsync(cancellationToken);
            return Ok(committees);
        }
    }
}
