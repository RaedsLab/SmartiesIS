using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ShoppingListService : IShoppingListService
    {

        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////

        public IEnumerable<ShoppingList> GetAll()
        {
            return utOfWork.ShoppingListRepository.GetAll();
        }
        public ShoppingList GetById(int id)
        {
            return utOfWork.ShoppingListRepository.GetById(id);
        }
        public void AddNew(ShoppingList p)
        {
            utOfWork.ShoppingListRepository.Add(p);
            utOfWork.Commit();
        }
        public void Delete(int id)
        {
            ShoppingList sl = utOfWork.ShoppingListRepository.GetById(id);
            utOfWork.ShoppingListRepository.Delete(sl);
            utOfWork.Commit();
        }
        public void Edit(ShoppingList p)
        {
            utOfWork.ShoppingListRepository.Update(p);
            utOfWork.Commit();
        }
    }
    public interface IShoppingListService
    {
        IEnumerable<ShoppingList> GetAll();
        ShoppingList GetById(int id);
        void AddNew(ShoppingList p);
        void Delete(int id);
        void Edit(ShoppingList p);

    }
}
