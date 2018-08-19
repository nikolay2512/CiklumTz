using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InternetShopParser.Model.Attributes
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(value);
            Validator.TryValidateObject(value, context, results, true);

            CompositeValidationResult compositeResults = null;

            if (results.Any())
            {
                compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", validationContext.DisplayName), new[] { validationContext.DisplayName });
                results.ForEach(compositeResults.AddResult);
            }

            return base.IsValid(value, validationContext);
        }

        public class CompositeValidationResult : ValidationResult
        {
            private readonly List<ValidationResult> _results = new List<ValidationResult>();

            public IEnumerable<ValidationResult> Results => _results;

            public CompositeValidationResult(string errorMessage) : base(errorMessage)
            {
            }

            public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
            {
            }

            public CompositeValidationResult(ValidationResult validationResult) : base(validationResult)
            {
            }

            public void AddResult(ValidationResult validationResult) => _results.Add(validationResult);
        }
    }
}
