using PuppyApi.Domain.Contracts.Validation;
using System;

namespace PuppyApi.Business.Validation
{
    public class DateEntryValidator : IValidator<DateTime>
    {
        /// <summary>
        /// Current rules: No future dates, and a maximum of 1 day old entries
        /// </summary>
        /// <param name="dateTime">The DateTime to inspect</param>
        /// <returns>true if validation rules pass</returns>
        public bool IsValid(DateTime dateTime)
        {
            var difference = dateTime - DateTime.Now;

            if (difference > TimeSpan.Zero || difference < TimeSpan.FromDays(-1))
                return false;

            return true;
        }
    }
}
