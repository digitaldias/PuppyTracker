using PuppyApi.Domain.Contracts.Validation;
using System;

namespace PuppyApi.Business.Validation
{
    public class DateEntryValidator : IValidator<DateTime>
    {
        public bool IsValid(DateTime dateTime)
        {
            var difference = dateTime - DateTime.Now;

            if (difference > TimeSpan.Zero || difference < TimeSpan.FromDays(-1))
                return false;

            return true;
        }
    }
}
