using PuppyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuppyApi.Domain.Contracts.Managers
{
    public interface ISimpleCache<TKey, TEntity>
    {
        Task<IEnumerable<PottyBreak>> GetAllAsync(Func<int, Task<IEnumerable<PottyBreak>>> getAllFunction);

        Task AddAsync(PottyBreak pottyBreak, Func<PottyBreak, Task<bool>> addFunction);

        Task RemoveAsync(PottyBreak pottyBreak, Func<PottyBreak, Task<bool>> removeFunction);

        Task<PottyBreak> GetByIdAsync(Guid verifiedGuid, Func<Guid, Task<PottyBreak>> getById);

        Task<IEnumerable<PottyBreak>> Fill(Func<int, Task<IEnumerable<PottyBreak>>> loaderTask);
    }
}
