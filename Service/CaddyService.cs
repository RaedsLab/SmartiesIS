using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CaddyService : ICaddyService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////
        public IEnumerable<Caddy> GetAll()
        {
            return utOfWork.CaddyRepository.GetAll();
        }
        public Caddy GetById(int id)
        {
            return utOfWork.CaddyRepository.GetById(id);
        }

        public void AddNew(Caddy c)
        {
            utOfWork.CaddyRepository.Add(c);
            utOfWork.Commit();
        }
        public void Delete(int id)
        {
            Caddy c = utOfWork.CaddyRepository.GetById(id);
            utOfWork.CaddyRepository.Delete(c);
            utOfWork.Commit();
        }

    }
    public interface ICaddyService
    {
        IEnumerable<Caddy> GetAll();
        Caddy GetById(int id);
        void AddNew(Caddy c);
        void Delete(int id);
      //  void Edit(Caddy c);

    }
}
