using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////

        public IEnumerable<Product> GetAllProducts()
        {
            var prods = utOfWork.ProductRepository.GetAll();
            return prods;
        }

        public Product GetProductById(int id)
        {
            return utOfWork.ProductRepository.GetById(id);
        }
        public void AddNewProduct(Product p)
        {
            utOfWork.ProductRepository.Add(p);
            utOfWork.Commit();
        }
        public void DeleteProd(int id)
        {
            Product p = this.GetProductById(id);
            if (p != null)
            {
                utOfWork.ProductRepository.Delete(p);
                utOfWork.Commit();
            }
        }

        public void EditProd(Product p)
        {
            utOfWork.ProductRepository.Update(p);
            utOfWork.Commit();
        }

    }
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddNewProduct(Product p);
        void DeleteProd(int id);
        void EditProd(Product p);


    }
}
