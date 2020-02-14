using PuppyApi.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace PuppyApi.Managers
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
