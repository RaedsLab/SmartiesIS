using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context dataContext;
        DatabaseFactory dbFactory;
        public UnitOfWork(DatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }


        /// 
        private IAdRepository AdRepository;
        IAdRepository IUnitOfWork.AdRepository
        {
            get { return AdRepository ?? (AdRepository = new AdRepository(dbFactory)); }
        }
        ///
        private IAdminRepository AdminRepository;
        IAdminRepository IUnitOfWork.AdminRepository
        {
            get { return AdminRepository ?? (AdminRepository = new AdminRepository(dbFactory)); }
        }
        ///
        private IAyleRepository AyleRepository;
        IAyleRepository IUnitOfWork.AyleRepository
        {
            get { return AyleRepository ?? (AyleRepository = new AyleRepository(dbFactory)); }
        }
        ///
        private ICaddyRepository CaddyRepository;
        ICaddyRepository IUnitOfWork.CaddyRepository
        {
            get { return CaddyRepository ?? (CaddyRepository = new CaddyRepository(dbFactory)); }
        }
        ///
        private IClientRepository ClientRepository;
        IClientRepository IUnitOfWork.ClientRepository
        {
            get { return ClientRepository ?? (ClientRepository = new ClientRepository(dbFactory)); }
        }
        ///
        private IItemShoppingRepository ItemShoppingRepository;
        IItemShoppingRepository IUnitOfWork.ItemShoppingRepository
        {
            get { return ItemShoppingRepository ?? (ItemShoppingRepository = new ItemShoppingRepository(dbFactory)); }
        }
        ///
        private ILogRepository LogRepository;
        ILogRepository IUnitOfWork.LogRepository
        {
            get { return LogRepository ?? (LogRepository = new LogRepository(dbFactory)); }
        }
        ///
        private IProductRepository ProductRepository;
        IProductRepository IUnitOfWork.ProductRepository
        {
            get { return ProductRepository ?? (ProductRepository = new ProductRepository(dbFactory)); }
        }
        ///
        private IShoppingListRepository ShoppingListRepository;
        IShoppingListRepository IUnitOfWork.ShoppingListRepository
        {
            get { return ShoppingListRepository ?? (ShoppingListRepository = new ShoppingListRepository(dbFactory)); }
        }
        ///


        protected Context DataContext
        {
            get
            {
                return dataContext ?? (dataContext = dbFactory.Get());
            }
        }
        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }

}
