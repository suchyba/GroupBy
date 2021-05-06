using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public Dictionary<string, string> ValidationErrors { get; set; }
        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new Dictionary<string, string>();

            foreach (var validationError in validationResult.Errors)
            {
                if(ValidationErrors.TryAdd(validationError.PropertyName, validationError.ErrorMessage) == false)
                {
                    ValidationErrors[validationError.PropertyName] += $"\n{validationError.ErrorMessage}";
                }
            }
        }
    }
}
