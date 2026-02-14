using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Validators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value == null)
                return true;

            var date = (DateTime)value;

            return date.Date >= DateTime.UtcNow.Date;
        }
    }
}