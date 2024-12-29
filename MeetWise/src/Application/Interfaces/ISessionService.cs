using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeetWise.Application.DTOs;

namespace MeetWise.Application.Interfaces
{
    public interface ISessionService
    {
        Task<int> CreateSessionAsync(SessionDto sessionDto, CancellationToken cancellationToken);
        Task UpdateSessionAsync(SessionDto sessionDto, CancellationToken cancellationToken);
        Task DeleteSessionAsync(int sessionId, CancellationToken cancellationToken);
        Task<SessionDto?> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken);
        Task<IEnumerable<SessionDto>> GetAllSessionsAsync(CancellationToken cancellationToken);
    }
}
