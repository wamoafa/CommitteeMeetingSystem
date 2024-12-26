using MeetWise.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetWise.Application.Services
{
    public class CommitteeService : ICommitteeService
    {
        private readonly IApplicationDbContext _context;

        public CommitteeService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCommitteeAsync(Committee committee)
        {
            _context.Committees.Add(committee);
            await _context.SaveChangesAsync();
            return committee.Id;
        }

        public async Task UpdateCommitteeAsync(Committee committee)
        {
            var existingCommittee = await _context.Committees.FindAsync(committee.Id);
            if (existingCommittee != null)
            {
                existingCommittee.Name = committee.Name;
                existingCommittee.MemberCount = committee.MemberCount;
                existingCommittee.SessionCount = committee.SessionCount;
                existingCommittee.IsActive = committee.IsActive;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCommitteeAsync(int id)
        {
            var committee = await _context.Committees.FindAsync(id);
            if (committee != null)
            {
                _context.Committees.Remove(committee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Committee> GetCommitteeByIdAsync(int id)
        {
            return await _context.Committees.FindAsync(id);
        }

        public async Task<List<Committee>> GetCommitteesAsync()
        {
            return await _context.Committees.ToListAsync();
        }
    }
}
