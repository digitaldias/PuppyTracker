using PuppyApi.Domain.Contracts.Managers;
using PuppyApi.Domain.Contracts.Repositories;
using PuppyApi.Domain.Contracts.Validation;
using PuppyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuppyApi.Business.Managers
{
    public class PottyBreaksManager : IPottyBreaksManager
    {
        private readonly IPottyBreakRepository          _pottyBreakRepository;
        private readonly IValidator<DateTime>           _dateEntryValidator;
        private readonly ISimpleCache<Guid, PottyBreak> _simpleCache;

        public PottyBreaksManager(IPottyBreakRepository pottyBreakRepository, IValidator<DateTime> dateEntryValidator, ISimpleCache<Guid, PottyBreak> simpleCache)
        {
            if (pottyBreakRepository is null)   throw new ArgumentNullException(nameof(pottyBreakRepository));
            if (dateEntryValidator is null)     throw new ArgumentNullException(nameof(dateEntryValidator));
            if (simpleCache is null)            throw new ArgumentException(nameof(simpleCache));

            _pottyBreakRepository = pottyBreakRepository;
            _dateEntryValidator   = dateEntryValidator;
            _simpleCache          = simpleCache;
        }

        public async Task DeleteAsync(PottyBreak pottyBreak)
        {
            //TODO: Log this
            if (pottyBreak is null)
                return;

            await _simpleCache.RemoveAsync(pottyBreak, _pottyBreakRepository.DeleteAsync);            
        }

        public async Task<IEnumerable<PottyBreak>> GetAllAsync(int max)
        {
            if (max <= 0)
                return new List<PottyBreak>();

            return await _simpleCache.GetAllAsync(_pottyBreakRepository.GetAllAsync);
        }

        public async Task<PottyBreak> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var couldParse = Guid.TryParseExact(id, "D", out Guid verifiedGuid);
            if (!couldParse)
                return null;

            return await _simpleCache.GetByIdAsync(verifiedGuid, _pottyBreakRepository.GetById);
        }

        public async Task SaveAsync(PottyBreak pottyBreak)
        {
            //TODO: Log?
            if (pottyBreak is null)
                return;

            if (!_dateEntryValidator.IsValid(pottyBreak.DateTime))
                return;

            await _simpleCache.AddAsync(pottyBreak, _pottyBreakRepository.SaveAsync);
        }
    }
}
