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

        [Key]
        [Column("Code")]
        public long? Code { get; set; }

        [Column("Name")]
        public int Name { get; set; }


        [Column("CreatedBY")]
        public string CreatedBY { get; set; }


        [Column("CreateOn")]
        public DateTimeOffset? CreateOn { get; set; }

        [Column("UpdatedBY")]
        public string UpdatedBY { get; set; }

        [Column("UpdatedOn")]
        public DateTimeOffset? UpdatedOn { get; set; }

        [Column("IsActive")]
        public Int16 IsActive { get; set; }

    }
}
