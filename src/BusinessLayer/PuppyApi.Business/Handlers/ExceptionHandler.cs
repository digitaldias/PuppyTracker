using PuppyApi.Domain.Contracts.Handlers;
using System;
using System.Threading.Tasks;

namespace PuppyApi.Business.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public Task<TResult> GetAsync<TResult>(Func<Task<TResult>> unsafeTask)
        {
            if (unsafeTask is null)
                throw new ArgumentNullException(nameof(unsafeTask));

            try
            {
                return unsafeTask.Invoke();
            }
            catch
            {
                //TODO: Logging? Console output?
            }
            return Task.FromResult<TResult>(default);
        }

        public Task RunAsync(Func<Task> unsafeFunction)
        {
            if (unsafeFunction is null)
                throw new ArgumentNullException(nameof(unsafeFunction));

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
