using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LogService : ILogService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////

        public IEnumerable<Log> GetAll()
        {
            return utOfWork.LogRepository.GetAll();
        }

        public Log GetById(int id)
        {
            return utOfWork.LogRepository.GetById(id);
        }

        public void AddNew(Log a)
        {
            utOfWork.LogRepository.Add(a);
            utOfWork.Commit();
        }
        public void Delete(int id)
        {
            Log l = utOfWork.LogRepository.GetById(id);
            utOfWork.LogRepository.Delete(l);
            utOfWork.Commit();
        }

    }
    public interface ILogService
    {
        IEnumerable<Log> GetAll();
        Log GetById(int id);
        void AddNew(Log a);
        void Delete(int id);

    }
}
