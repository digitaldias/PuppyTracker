using Microsoft.Extensions.Configuration;
using PuppyApi.Domain.Contracts.Handlers;
using PuppyApi.Domain.Contracts.Managers;
using PuppyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Business.Managers
{
    public class PottyBreaksCache : ISimpleCache<Guid, PottyBreak>
    {
        private readonly IExceptionHandler _exceptionHandler;
        private int                        _maxSize = 20;
        private List<PottyBreak>           _pottyBreaks;

        public PottyBreaksCache(IConfiguration configuration, IExceptionHandler exceptionHandler)
        {
            if (exceptionHandler is null) throw new ArgumentNullException(nameof(exceptionHandler));

            if (!int.TryParse(configuration["PottyBreaks:CacheSize"], out _maxSize))
                throw new ArgumentException("Unable to read setting: PottyBreaks:CacheSize");

            _pottyBreaks      = new List<PottyBreak>();
            _exceptionHandler = exceptionHandler;
        }

        public async Task<IEnumerable<PottyBreak>> GetAllAsync(Func<int, Task<IEnumerable<PottyBreak>>> getAllFunction)
        {
            // If nothing, the fill the cache using the supplied function
            if (_pottyBreaks is null || !_pottyBreaks.Any())
                await Fill(getAllFunction);

            return await Task.FromResult(_pottyBreaks);
        }

        public async Task<PottyBreak> GetByIdAsync(Guid guid, Func<Guid, Task<PottyBreak>> getById)
        {
            // Yup, it's  cached
            var match = _pottyBreaks.FirstOrDefault(pb => pb.Id == guid);
            if (match is { })
                return match;

            // Not found, use the repo
            var pottyBreak = await getById.Invoke( guid);
            
            // Insert it into our cache
            if (pottyBreak is { })
                Insert(pottyBreak);

            return pottyBreak;            
        }

        public async Task<IEnumerable<PottyBreak>> Fill(Func<int, Task<IEnumerable<PottyBreak>>> loaderTask)
        {
            var pottyBreaks = await _exceptionHandler.GetAsync(() => loaderTask.Invoke(_maxSize));
            _pottyBreaks    = new List<PottyBreak>();

            if (pottyBreaks is { } && pottyBreaks.Any())
                _pottyBreaks.AddRange(pottyBreaks.OrderByDescending(p => p.DateTime));
            
            return _pottyBreaks;            
        }

        public async Task RemoveAsync(PottyBreak pottyBreak, Func<PottyBreak, Task<bool>> removeFunction)
        {
            if (await _exceptionHandler.GetAsync(() => removeFunction.Invoke(pottyBreak))) 
                _pottyBreaks.Remove(pottyBreak);
        }

        public async Task AddAsync(PottyBreak pottyBreak, Func<PottyBreak, Task<bool>> addFunction)
        {
            if (await _exceptionHandler.GetAsync(() => addFunction.Invoke(pottyBreak))) 
                Insert(pottyBreak);
        }

        private void Insert(PottyBreak pottyBreak)
        {
            var found = _pottyBreaks.FirstOrDefault(i => i.Id == pottyBreak.Id);
            if (found is { })
                _pottyBreaks.Remove(found);

            _pottyBreaks.Add(pottyBreak);
            _pottyBreaks = _pottyBreaks.OrderByDescending(p => p.DateTime).ToList();

            if (_pottyBreaks.Count > _maxSize)
                _pottyBreaks.RemoveAt(_pottyBreaks.Count - 1);
        }
    }
}
