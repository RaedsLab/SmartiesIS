namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Ad = new HashSet<Ad>();
            ItemShopping = new HashSet<ItemShopping>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        public double? Price { get; set; }

        public bool? Promoted { get; set; }

        public string Details { get; set; }

        public int? xPos { get; set; }

        public int? yPos { get; set; }

        public int? zPos { get; set; }

        public int? rPos { get; set; }

        public string Image { get; set; }

        public int? AyleId { get; set; }

        public virtual ICollection<Ad> Ad { get; set; }

        public virtual Ayle Ayle { get; set; }

        public virtual ICollection<ItemShopping> ItemShopping { get; set; }
    }
}
