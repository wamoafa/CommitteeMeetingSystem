using MeetWise.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetWise.Application.Interfaces
{
    public interface ICommitteeRepository
    {
        Task<List<Committee>> GetAllAsync();
        Task<Committee> GetByIdAsync(int id);
        Task AddAsync(Committee committee);
        Task UpdateAsync(Committee committee);
        Task DeleteAsync(int id);
    }
}
