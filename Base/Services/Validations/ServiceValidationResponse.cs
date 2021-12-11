using System.Collections.Generic;
using System.Linq;

namespace Base.Services.Validations
{
    /// <summary>
    /// The response of the event catalog get event booking data.
    /// </summary>
    public class ServiceValidationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidationResponse"/> class.
        /// </summary>
        public ServiceValidationResponse()
        {
            this.ValidationResults = new List<ServiceValidationResult>();
        }

        /// <summary>
        /// Gets the validation results.
        /// </summary>
        /// <value>
        /// The validation results.
        /// </value>
        public ICollection<ServiceValidationResult> ValidationResults
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the model is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid
        {
            get
            {
                if (this.ValidationResults == null || this.ValidationResults.Count == 0)
                {
                    return true;
                }

                return this.ValidationResults.All((validationResult) => validationResult.MessageSeverity != ValidationSeverityValue.Error);
            }
        }

        /// <summary>
        /// Adds the validation results.
        /// </summary>
        /// <param name="serviceValidationResult">The service validation result.</param>
        public void AddValidationResults(params ServiceValidationResult[] serviceValidationResult)
        {
            foreach (var item in serviceValidationResult)
            {
                this.ValidationResults.Add(item);
            }
        }

        /// <summary>
        /// Adds the validation results.
        /// </summary>
        /// <param name="serviceValidationResult">The service validation result.</param>
        public void AddValidationResults(IEnumerable<ServiceValidationResult> serviceValidationResult)
        {
            foreach (var item in serviceValidationResult)
            {
                this.ValidationResults.Add(item);
            }
        }
    }
}
