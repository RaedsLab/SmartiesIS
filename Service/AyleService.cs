using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AyleService : IAyleService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////
        public IEnumerable<Ayle> GetAll()
        {
            return utOfWork.AyleRepository.GetAll();
        }
        public Ayle GetById(int id)
        {
            return utOfWork.AyleRepository.GetById(id);
        }
        public void AddNew(Ayle a)
        {
            utOfWork.AyleRepository.Add(a);
            utOfWork.Commit();
        }
        public void Delete(int id)
        {
            Ayle a = utOfWork.AyleRepository.GetById(id);
            utOfWork.AyleRepository.Delete(a);
            utOfWork.Commit();

        }
        public void Edit(Ayle p) /// @TODO : See exp
        {
            utOfWork.AyleRepository.Update(p);
            utOfWork.Commit();
        }
    }
    public interface IAyleService
    {
        IEnumerable<Ayle> GetAll();
        Ayle GetById(int id);
        void AddNew(Ayle a);
        void Delete(int id);
        void Edit(Ayle p);
    }
}
