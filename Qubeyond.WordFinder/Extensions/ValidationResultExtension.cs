using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubeyond.WordFinder.Extensions
{
    public static class ValidationResultExtension
    {
        public static string ConcatErrors(this ValidationResult? validationResult)
        {
            if (validationResult == null)
            {
                return string.Empty;
            }

            return string.Join(",", validationResult.Errors);
        }
    }
}
