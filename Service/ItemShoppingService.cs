using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ItemShoppingService : IItemShoppingService 
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////

        

        public IEnumerable<ItemShopping> GetAll()
        {
            return utOfWork.ItemShoppingRepository.GetAll();
        }

        public ItemShopping GetById(int id)
        {
            return utOfWork.ItemShoppingRepository.GetById(id);
        }

        public void AddNew(ItemShopping a)
        {
            utOfWork.ItemShoppingRepository.Add(a);
            utOfWork.Commit();
        }
        public void Delete(int id)
        {
            ItemShopping isa = utOfWork.ItemShoppingRepository.GetById(id);
            utOfWork.ItemShoppingRepository.Delete(isa);
            utOfWork.Commit();
        }
        public void Edit(ItemShopping p)
        {
            utOfWork.ItemShoppingRepository.Update(p);
            utOfWork.Commit();
        }
        public void Dispose()
        {
            utOfWork.Dispose();
        }

    }

    public interface IItemShoppingService : IDisposable
    {
        IEnumerable<ItemShopping> GetAll();
        ItemShopping GetById(int id);
        void AddNew(ItemShopping a);
        void Delete(int id);
        void Edit(ItemShopping p);
    }
}
