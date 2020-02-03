using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Managers
{
    public interface IExceptionHandler
    {
        Task<TResult> GetAsync<TResult>(Func<Task<TResult>> unsafeFask);

        Task RunAsync(Func<Task> unsafeFunction);
    }
}
