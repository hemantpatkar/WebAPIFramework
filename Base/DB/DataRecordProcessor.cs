using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Base.DB
{
    /// <summary>
    /// Captures the processing logic of each data record, as well as preprocessing and postprocessing.
    /// </summary>
    public class DataRecordProcessor
    {
        /// <summary>
        /// Gets or sets the pre process records.
        /// </summary>
        /// <value>
        /// The pre process records.
        /// </value>
        public Func<IDataRecord, CancellationToken, Task> PreProcessRecords { get; set; }

        /// <summary>
        /// Gets or sets the process record.
        /// </summary>
        /// <value>
        /// The process record.
        /// </value>
        public Func<IDataRecord, CancellationToken, Task> ProcessRecord { get; set; }

        /// <summary>
        /// Gets or sets the post process records.
        /// </summary>
        /// <value>
        /// The post process records.
        /// </value>
        public Func<CancellationToken, Task> PostProcessRecords { get; set; }
    }
}
