using PuppyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuppyApi.Domain.Contracts.Repositories
{
    public interface IPottyBreakRepository
    {
        Task InitializeAsync();

        Task<IEnumerable<PottyBreak>> GetAllAsync(int max);

        Task<PottyBreak> GetById(Guid verifiedGuid);
        
        Task<bool> SaveAsync(PottyBreak pottyBreak);
        
        Task<bool> DeleteAsync(PottyBreak pottyBreak);
    }
}