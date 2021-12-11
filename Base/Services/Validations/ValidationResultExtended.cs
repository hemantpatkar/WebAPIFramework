using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Base.Services.Validations
{
    /// <summary>
    /// This class represents a common validation result with severity.
    /// </summary>
    public class ValidationResultExtended : ValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultExtended"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="memberNames">The list of member names that have validation errors.</param>
        public ValidationResultExtended(string errorMessage, IEnumerable<string> memberNames)
            : base(errorMessage, memberNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultExtended"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="memberNames">The member names.</param>
        /// <param name="severity">The severity.</param>
        public ValidationResultExtended(string errorMessage, IEnumerable<string> memberNames, ValidationSeverityValue severity)
            : base(errorMessage, memberNames)
        {
            this.Severity = severity;
        }

        /// <summary>
        /// Gets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public ValidationSeverityValue Severity
        {
            get;
            private set;
        }
    }
}
