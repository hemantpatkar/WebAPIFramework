using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.DomainModels.Master
{

    [Table("mcountry", Schema = "dbo")]
    public class CountryMaster
    {
        [Key]
        [Column("ID")]
        public long? ID { get; set; }
        
        [Column("Code")]
        public string Code { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("CreatedBY")]
        public string CreatedBY { get; set; }

        [Column("CreatedOn")]
        public DateTimeOffset? CreatedOn { get; set; }

        [Column("UpdatedBY")]
        public string UpdatedBY { get; set; }

        [Column("UpdatedOn")]
        public DateTimeOffset? UpdatedOn { get; set; }

        [Column("IsActive")]
        public byte IsActive { get; set; }

    }
}
