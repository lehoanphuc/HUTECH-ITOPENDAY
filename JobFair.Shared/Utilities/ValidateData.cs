using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JobFair.Shared.Utilities
{
    /// <summary>
    /// Read more: https://www.geekinsta.com/manually-validate-with-data-annotations/
    /// </summary>
    public class ValidateData
    {
        public static bool Validate(object value, bool _throw = false)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, context, results, true);

            if (results.Count != 0)
            {
                if (_throw)
                {
                    throw new Exception(results.First().ErrorMessage);
                }
                return false;
            }

            return true;
        }
    }
}
