using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IClientRepository : IRepository<Client> { }

}
