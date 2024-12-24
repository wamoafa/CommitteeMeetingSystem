using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetWise.Models; 


namespace MeetWise.Application.Interfaces
{
    public interface ICommitteeRepository
    {
        Task<List<Committee>> GetAllAsync();
        Task<Committee> GetByIdAsync(int id);
        Task AddAsync(Committee committee);
        Task UpdateAsync(Committee committee);
        Task DeleteAsync(int id); // Soft delete
    }
}
