namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Admin")]
    public partial class Admin
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string Password { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? IsVendor { get; set; }
    }
}
