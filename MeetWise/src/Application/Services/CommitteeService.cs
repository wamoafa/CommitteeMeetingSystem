using MeetWise.Application.Common.Interfaces;
using MeetWise.Application.Interfaces;
using MeetWise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeetWise.Application.Services
{
    public class CommitteeService : ICommitteeService
    {
        private readonly IApplicationDbContext _context;

        public CommitteeService(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CreateCommitteeAsync(Committee committee, CancellationToken cancellationToken)
        {
            if (committee == null)
                throw new ArgumentNullException(nameof(committee));

            _context.Committees.Add(committee);
            await _context.SaveChangesAsync(cancellationToken);
            return committee.Id;
        }

        public async Task UpdateCommitteeAsync(Committee committee, CancellationToken cancellationToken)
        {
            if (committee == null)
                throw new ArgumentNullException(nameof(committee));

            _context.Committees.Update(committee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCommitteeAsync(int committeeId, CancellationToken cancellationToken)
        {
            var committee = await _context.Committees.FindAsync(new object[] { committeeId }, cancellationToken);
            if (committee == null)
                throw new KeyNotFoundException($"Committee with ID {committeeId} not found.");

            _context.Committees.Remove(committee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Committee> GetCommitteeByIdAsync(int committeeId, CancellationToken cancellationToken)
        {
            var committee = await _context.Committees
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.Id == committeeId, cancellationToken);

            if (committee == null)
                throw new KeyNotFoundException($"Committee with ID {committeeId} not found.");

            return committee;
        }

        
        public async Task<List<CommitteeDto>> GetAllCommitteesAsync(CancellationToken cancellationToken)
        {
            var committees = await _context.Committees
                .Include(c => c.Members)
                .ToListAsync(cancellationToken);

            return committees.Select(c => new CommitteeDto
            {
                Id = c.Id,
                Name = c.Name,
                Details = c.Details,
                IsActive = c.IsActive,
                Members = c.Members.Select(m => new MemberDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    NationalId = m.NationalId,
                    Username = m.Username,
                    PhoneNumber = m.PhoneNumber,
                    IsActive = m.IsActive
                }).ToList()
            }).ToList();
        }

        Task<List<global::CommitteeDto>> ICommitteeService.GetAllCommitteesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
