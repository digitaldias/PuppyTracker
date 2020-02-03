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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                //TODO: Logging? Console output?
            }
            return Task.CompletedTask;
        }
    }
}
