using System;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Hosting.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotBeforeTodayAttribute : ValidationAttribute
    {
        public NotBeforeTodayAttribute() 
            : base("Cannot be before today")
        {
        }

        public override bool IsValid(object value)
        {
            return value is DateTimeOffset date && date.Date >= DateTimeOffset.Now.Date;
        }
    }
}