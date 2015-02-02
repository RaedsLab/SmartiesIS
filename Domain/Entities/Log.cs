namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Log")]
    public partial class Log
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Trigger { get; set; }

        [StringLength(50)]
        public string Cide { get; set; }

        public DateTime? Time { get; set; }

        public string Message { get; set; }
    }
}
