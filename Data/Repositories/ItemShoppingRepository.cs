using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class ItemShoppingRepository : RepositoryBase<ItemShopping>, IItemShoppingRepository
    {
        public ItemShoppingRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IItemShoppingRepository : IRepository<ItemShopping> { }

}
