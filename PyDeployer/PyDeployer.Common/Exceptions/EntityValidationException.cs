using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PyDeployer.Common.Exceptions
{
    public class EntityValidationException : Exception
    {
        private readonly IEnumerable<ValidationResult> _validationResults;

        public IEnumerable<ValidationResult> ValidationResults => _validationResults;

        public EntityValidationException(IEnumerable<ValidationResult> validationResults = null)
        {
            _validationResults = validationResults ?? new List<ValidationResult>();
        }

        public EntityValidationException(string message, IEnumerable<ValidationResult> validationResults = null)
            : base(message)
        {
            _validationResults = validationResults ?? new List<ValidationResult>();
        }

        public EntityValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            _validationResults = new List<ValidationResult>();
        }
        
    }
}