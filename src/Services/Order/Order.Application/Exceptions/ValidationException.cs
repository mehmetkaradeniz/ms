using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("One or more validation failures have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(i => i.PropertyName, i => i.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            foreach (var failure in failures)
                Errors.Add(failure.PropertyName, new string[] { failure.ErrorMessage });
        }

        public Dictionary<string, string[]> Errors { get; }
    }
}
