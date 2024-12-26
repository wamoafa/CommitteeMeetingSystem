using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IActionResult> CreateCommittee([FromBody] Committee committee)
        {
            var id = await _committeeService.CreateCommitteeAsync(committee);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCommittee([FromBody] Committee committee)
        {
            await _committeeService.UpdateCommitteeAsync(committee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommittee(int id)
        {
            await _committeeService.DeleteCommitteeAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommitteeById(int id)
        {
            var committee = await _committeeService.GetCommitteeByIdAsync(id);
            return Ok(committee);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommittees()
        {
            var committees = await _committeeService.GetCommitteesAsync();
            return Ok(committees);
        }
    }
}
