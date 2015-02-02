using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ClientService : IClientService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////
        public IEnumerable<Client> GetAll()
        {
            return utOfWork.ClientRepository.GetAll();
        }

        public Client GetById(int id)
        {
            return utOfWork.ClientRepository.GetById(id);
        }
        public void AddNew(Client a)
        {
            utOfWork.ClientRepository.Add(a);
            utOfWork.Commit();
        }

        public void Delete(int id)
        {
            Client c = utOfWork.ClientRepository.GetById(id);
            utOfWork.Commit();
        }
        public void Edit(Client p)
        {
            utOfWork.ClientRepository.Update(p);
            utOfWork.Commit();
        }
        public Client logClient(string email, string pwd)
        {
            return utOfWork.ClientRepository.Get(c => c.Email.Equals(email) && c.Password.Equals(pwd));
        }

    }

    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void AddNew(Client a);
        void Delete(int id);
        void Edit(Client p);

        Client logClient(string email, string pwd);

    }
}
