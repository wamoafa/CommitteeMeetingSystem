﻿using MeetWise.Application.Interfaces;
using MeetWise.Domain.Entities;
using MeetWise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetWise.Infrastructure.Persistence.Repositories
{
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly ApplicationDbContext _context;

        public CommitteeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Committee>> GetAllAsync()
        {
            return await _context.Committees
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Committee> GetByIdAsync(int id)
        {
            var committee = await _context.Committees
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (committee == null)
            {
                throw new KeyNotFoundException($"Committee with ID {id} not found.");
            }

            return committee;
        }


        public async Task AddAsync(Committee committee)
        {
            _context.Committees.Add(committee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Committee committee)
        {
            _context.Committees.Update(committee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var committee = await _context.Committees.FindAsync(id);
            if (committee != null)
            {
                committee.IsDeleted = true;
                _context.Committees.Update(committee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
