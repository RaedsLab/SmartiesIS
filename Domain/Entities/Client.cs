namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            Caddy = new HashSet<Caddy>();
            ShoppingList = new HashSet<ShoppingList>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FamilyName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Tag { get; set; }

        [StringLength(50)]
        public string Privacy { get; set; }

        public int? CaddyId { get; set; }

        public virtual ICollection<Caddy> Caddy { get; set; }

        public virtual ICollection<ShoppingList> ShoppingList { get; set; }
    }
}
