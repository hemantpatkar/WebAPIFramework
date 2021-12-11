using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Services.Validations
{
    /// <summary>
    /// Enumeration of possible values for service validation result messages severity.
    /// </summary>
    public enum ValidationSeverityValue
    {
        /// <summary>
        /// The service validation result messages severity error.
        /// </summary>
        Error = 0,

        /// <summary>
        /// The service validation result messages severity warning.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// The service validation result messages severity information.
        /// </summary>
        Information = 2,
    }
}
