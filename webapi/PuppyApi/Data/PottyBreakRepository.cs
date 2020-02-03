using PuppyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Data
{
    public class PottyBreakRepository : IPottyBreakRepository
    {
        public PottyBreakRepository()
        {

        }

        public Task DeleteAsync(PottyBreak pottyBreak)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PottyBreak>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PottyBreak> GetById(Guid verifiedGuid)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(PottyBreak pottyBreak)
        {
            throw new NotImplementedException();
        }
    }
}
