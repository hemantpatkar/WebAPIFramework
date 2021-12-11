using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Services
{
    /// <summary>
    /// This class represents the response of a service with result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class ServiceDataResponse<TResult> : ServiceResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataResponse{TResult}"/> class.
        /// </summary>
        public ServiceDataResponse()
            : this(default(TResult))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataResponse{TResult}" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ServiceDataResponse(TResult result)
            : base()
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public TResult Result
        {
            get;
            set;
        }
    }
}
