namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.Entities;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false; 
        }

        public virtual DbSet<Ad> Ad { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Ayle> Ayle { get; set; }
        public virtual DbSet<Caddy> Caddy { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ItemShopping> ItemShopping { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ShoppingList> ShoppingList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>()
                .Property(e => e.Etat)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Ayle>()
                .Property(e => e.yPos)
                .IsFixedLength();

            modelBuilder.Entity<Ayle>()
                .Property(e => e.zPos)
                .IsFixedLength();

            modelBuilder.Entity<Ayle>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<Ayle>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Caddy>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.FamilyName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Privacy)
                .IsUnicode(false);

            modelBuilder.Entity<ItemShopping>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<ItemShopping>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Trigger)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Cide)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<ShoppingList>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
