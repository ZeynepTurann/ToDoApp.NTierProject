using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierCommon.ResponseObjects;

namespace ToDoAppNTierBLL.Extensions
{
    public static class ValidatonResultExtension
    {
        /// <summary>
        ///Convert the error list in the argument that comes as a parameter to CustomValidationError List
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns> List of CustomValidationErrors</returns>
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationError> errors = new();
            foreach (var item in validationResult.Errors)
            {
                errors.Add(new()
                {
                    ErrorMessage = item.ErrorMessage,
                    PropertyName = item.PropertyName,
                });
            }
            return errors;
        }
    }
}
