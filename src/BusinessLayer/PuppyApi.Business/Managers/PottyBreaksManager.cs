using PuppyApi.Domain.Contracts.Handlers;
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
        private readonly IExceptionHandler     _exceptionHandler;
        private readonly IPottyBreakRepository _pottyBreakRepository;
        private readonly IValidator<DateTime> _dateEntryValidator;

        public PottyBreaksManager(IExceptionHandler exceptionHandler, IPottyBreakRepository pottyBreakRepository, IValidator<DateTime> dateEntryValidator)
        {
            if (exceptionHandler is null)       throw new ArgumentNullException(nameof(exceptionHandler));
            if (pottyBreakRepository is null)   throw new ArgumentNullException(nameof(pottyBreakRepository));
            if (dateEntryValidator is null)     throw new ArgumentNullException(nameof(dateEntryValidator));

            _exceptionHandler     = exceptionHandler;
            _pottyBreakRepository = pottyBreakRepository;
            _dateEntryValidator   = dateEntryValidator;
        }

        public async Task DeleteAsync(PottyBreak pottyBreak)
        {
            //TODO: Log this
            if (pottyBreak is null)
                return;

            await _exceptionHandler.RunAsync(() => _pottyBreakRepository.DeleteAsync(pottyBreak));
        }

        public async Task<IEnumerable<PottyBreak>> GetAllAsync(int max)
        {
            if (max <= 0)
                return new List<PottyBreak>();

            return await _exceptionHandler.GetAsync(() => _pottyBreakRepository.GetAllAsync(max));
        }

        public async Task<PottyBreak> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var couldParse = Guid.TryParseExact(id, "D", out Guid verifiedGuid);
            if (!couldParse)
                return null;

            return await _exceptionHandler.GetAsync(() => _pottyBreakRepository.GetById(verifiedGuid));
        }

        public async Task SaveAsync(PottyBreak pottyBreak)
        {
            //TODO: Log?
            if (pottyBreak is null)
                return;

            if (!_dateEntryValidator.IsValid(pottyBreak.DateTime))
                return;

            await _exceptionHandler.RunAsync(() => _pottyBreakRepository.SaveAsync(pottyBreak));
        }
    }
}
