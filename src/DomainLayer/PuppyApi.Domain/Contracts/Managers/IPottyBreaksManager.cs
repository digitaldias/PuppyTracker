using PuppyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuppyApi.Domain.Contracts.Managers
{
    public interface IPottyBreaksManager
    {
        Task<IEnumerable<PottyBreak>> GetAllAsync();

        Task<PottyBreak> GetByIdAsync(string id);

        Task SaveAsync(PottyBreak pottyBreak);

        Task DeleteAsync(PottyBreak pottyBreak);
    }
}
