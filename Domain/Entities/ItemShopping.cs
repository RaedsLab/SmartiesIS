namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemShopping")]
    public partial class ItemShopping
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public int? Quantity { get; set; }

        public int? ProductId { get; set; }

        public int? ShoppingListId { get; set; }

        public virtual Product Product { get; set; }

        public virtual ShoppingList ShoppingList { get; set; }
    }
}
