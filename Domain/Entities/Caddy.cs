namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Caddy")]
    public partial class Caddy
    {
        public int Id { get; set; }

        public int? xPos { get; set; }

        public int? yPos { get; set; }

        public int? zPos { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
