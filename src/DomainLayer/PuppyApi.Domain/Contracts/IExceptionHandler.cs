using System;
using System.Threading.Tasks;

namespace PuppyApi.Domain.Contracts
{
    public interface IExceptionHandler
    {
        Task<TResult> GetAsync<TResult>(Func<Task<TResult>> unsafeFask);

        Task RunAsync(Func<Task> unsafeFunction);
    }
}
