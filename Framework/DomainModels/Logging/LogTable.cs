using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.DomainModels.Logging
{
     
    [Table("General_Log", Schema = "dbo")]
    public class LogTable
    {
        
        public enum Status : short
        {
            
            Info = 0,

         
            Error = 1,

           
            Warning = 2,

          
            Exception = 3,
        }

        
        [Key]
        [Column("CODE")]
        public long? Code { get; set; }

       
       
        [Column("LogStatus")]
        public int logstatus { get; set; }

      
        [Column("LogString")]
        public string LogString { get; set; }

       
        [Column("CreatedBY")]
        public string CreatedBY { get; set; }

      
        [Column("CreateOn")]
        public DateTimeOffset? CreateOn { get; set; }

       

    }
}
