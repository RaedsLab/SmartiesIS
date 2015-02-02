namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ayle")]
    public partial class Ayle
    {
        public Ayle()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }

        public int? xPos { get; set; }

        [StringLength(10)]
        public string yPos { get; set; }

        [StringLength(10)]
        public string zPos { get; set; }

        public string ShopName { get; set; }

        public string Category { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
