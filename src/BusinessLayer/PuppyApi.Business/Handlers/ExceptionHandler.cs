using PuppyApi.Domain.Contracts;
using PuppyApi.Domain.Contracts.Handlers;
using System;
using System.Threading.Tasks;

namespace PuppyApi.Business.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public Task<TResult> GetAsync<TResult>(Func<Task<TResult>> unsafeFask)
        {
            try
            {
                return unsafeFask.Invoke();
            }
            catch
            {
                //TODO: Logging? Console output?
            }
            return null;
        }

        public Task RunAsync(Func<Task> unsafeFunction)
        {
            try
            {
                return unsafeFunction.Invoke();
            }
            catch
            {
                //TODO: Logging? Console output?
            }
            return Task.CompletedTask;
        }
    }
}
