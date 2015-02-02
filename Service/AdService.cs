using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdService : IAdService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////

        public IEnumerable<Ad> GetAll()
        {
            return utOfWork.AdRepository.GetAll();
        }

        public Ad GetById(int id)
        {
            return utOfWork.AdRepository.GetById(id);
        }
        public void Delete(int id)
        {
            Ad a = utOfWork.AdRepository.GetById(id);
            utOfWork.AdRepository.Delete(a);
            utOfWork.Commit();
        }
    }

    public interface IAdService
    {
        IEnumerable<Ad> GetAll();
        Ad GetById(int id);
      //  void AddNew(Ad a);
        void Delete(int id);
    }
}
