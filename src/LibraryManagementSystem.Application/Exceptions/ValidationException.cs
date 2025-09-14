using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Application.Exceptions
{
    /// <summary>
    /// Represents validation errors for requests.
    /// </summary>
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation errors occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors;
        }

        public ValidationException(string message, IDictionary<string, string[]> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
