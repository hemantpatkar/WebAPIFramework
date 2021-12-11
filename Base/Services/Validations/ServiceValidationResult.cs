using System.Collections.Generic;

namespace Base.Services.Validations
{
    /// <summary>
    /// This class represents the service validation result.
    /// </summary>
    public class ServiceValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidationResult"/> class.
        /// </summary>
        /// <remarks>Needed for WCF serialization. Do not remove.</remarks>
        public ServiceValidationResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidationResult"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="memberNames">The member names.</param>
        public ServiceValidationResult(string errorMessage, params string[] memberNames)
            : this(errorMessage, ValidationSeverityValue.Error, memberNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidationResult" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="messageSeverity">The message severity.</param>
        /// <param name="memberNames">The member names.</param>
        public ServiceValidationResult(string errorMessage, ValidationSeverityValue messageSeverity, params string[] memberNames)
        {
            this.ErrorMessage = errorMessage;
            this.MemberNames = memberNames;
            this.MessageSeverity = messageSeverity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidationResult"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ServiceValidationResult(System.ComponentModel.DataAnnotations.ValidationResult result)
        {
            this.ErrorMessage = result.ErrorMessage;
            this.MemberNames = result.MemberNames;
            this.MessageSeverity = ValidationSeverityValue.Error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidationResult"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ServiceValidationResult(ValidationResultExtended result)
        {
            this.ErrorMessage = result.ErrorMessage;
            this.MemberNames = result.MemberNames;
            this.MessageSeverity = result.Severity;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the message severity.
        /// </summary>
        /// <value>
        /// The message severity.
        /// </value>
        public ValidationSeverityValue MessageSeverity { get; set; }

        /// <summary>
        /// Gets or sets the member names.
        /// </summary>
        /// <value>
        /// The member names.
        /// </value>
        public IEnumerable<string> MemberNames { get; set; }
    }
}
