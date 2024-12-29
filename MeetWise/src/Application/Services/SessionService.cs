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
    public class SessionService : ISessionService
    {
        private readonly IApplicationDbContext _context;

        public SessionService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSessionAsync(SessionDto sessionDto, CancellationToken cancellationToken)
        {
            var session = new Session
            {
                Name = sessionDto.Name,
                Date = sessionDto.Date,
                Details = sessionDto.Details,
                IsActive = sessionDto.IsActive,
                IsDeleted = sessionDto.IsDeleted,
                CommitteeId = sessionDto.CommitteeId
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(cancellationToken);
            return session.Id;
        }

        public async Task UpdateSessionAsync(SessionDto sessionDto, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions.FindAsync(new object[] { sessionDto.Id }, cancellationToken);
            if (session == null)
                throw new KeyNotFoundException($"Session with Id {sessionDto.Id} not found.");

            session.Name = sessionDto.Name;
            session.Date = sessionDto.Date;
            session.Details = sessionDto.Details;
            session.IsActive = sessionDto.IsActive;
            session.IsDeleted = sessionDto.IsDeleted;
            session.CommitteeId = sessionDto.CommitteeId;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSessionAsync(int sessionId, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions.FindAsync(new object[] { sessionId }, cancellationToken);
            if (session == null)
                throw new KeyNotFoundException($"Session with Id {sessionId} not found.");

            session.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<SessionDto?> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions
                .AsNoTracking()
                .Where(s => s.Id == sessionId && !s.IsDeleted)
                .Select(s => new SessionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Date = s.Date,
                    Details = s.Details,
                    IsActive = s.IsActive,
                    IsDeleted = s.IsDeleted,
                    CommitteeId = s.CommitteeId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return session;
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessionsAsync(CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .Select(s => new SessionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Date = s.Date,
                    Details = s.Details,
                    IsActive = s.IsActive,
                    IsDeleted = s.IsDeleted,
                    CommitteeId = s.CommitteeId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
