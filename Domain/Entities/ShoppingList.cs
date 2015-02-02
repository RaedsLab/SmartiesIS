namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ShoppingList")]
    public partial class ShoppingList
    {
        public ShoppingList()
        {
            ItemShopping = new HashSet<ItemShopping>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? Finished { get; set; }

        public bool? Private { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<ItemShopping> ItemShopping { get; set; }
    }
}
