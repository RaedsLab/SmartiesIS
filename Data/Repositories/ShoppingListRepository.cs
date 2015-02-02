using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class ShoppingListRepository : RepositoryBase<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IShoppingListRepository : IRepository<ShoppingList> { }

}
