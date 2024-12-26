using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetWise.Application.Common.Interfaces
{
    public interface ICommitteeService
    {
        Task<int> CreateCommitteeAsync(Committee committee);
        Task UpdateCommitteeAsync(Committee committee);
        Task DeleteCommitteeAsync(int id);
        Task<Committee> GetCommitteeByIdAsync(int id);
        Task<List<Committee>> GetCommitteesAsync();
    }
}
