using Base.Services.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Base.Services
{
    /// <summary>
    /// The response of the event catalog get event booking data.
    /// </summary>
    public class ServiceResponse : ServiceValidationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponse"/> class.
        /// </summary>
        public ServiceResponse()
            : base()
        {
            this.AuthorizationResults = new List<ServiceValidationResult>();
        }

        /// <summary>
        /// Gets the validation results.
        /// </summary>
        /// <value>
        /// The validation results.
        /// </value>
        public ICollection<ServiceValidationResult> AuthorizationResults
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
        public override bool IsValid
        {
            get
            {
                if (!IsAuthorized)
                {
                    return false;
                }

                return base.IsValid;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the model is authorized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model is authorized; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsAuthorized
        {
            get
            {
                if (this.AuthorizationResults == null || this.AuthorizationResults.Count == 0)
                {
                    return true;
                }

                return this.AuthorizationResults.All((authorizationResult) => authorizationResult.MessageSeverity != ValidationSeverityValue.Error);
            }
        }

        /// <summary>
        /// Adds the authorization results.
        /// </summary>
        /// <param name="serviceAuthorizationResult">The service authorization result.</param>
        public void AddAuthorizationResults(ICollection<ServiceValidationResult> serviceAuthorizationResult)
        {
            if (serviceAuthorizationResult != null)
            {
                foreach (var item in serviceAuthorizationResult)
                {
                    this.AuthorizationResults.Add(item);
                }
            }
        }

        /// <summary>
        /// Adds the validation results.
        /// </summary>
        /// <param name="validationResultExtended">The validation result extended.</param>
        public void AddValidationResults(params ValidationResultExtended[] validationResultExtended)
        {
            if (validationResultExtended != null)
            {
                foreach (var item in validationResultExtended)
                {
                    this.ValidationResults.Add(new ServiceValidationResult(item));
                }
            }
        }
    }
}
