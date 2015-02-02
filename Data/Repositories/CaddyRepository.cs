using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class CaddyRepository : RepositoryBase<Caddy>, ICaddyRepository
    {
        public CaddyRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface ICaddyRepository : IRepository<Caddy> { }

}

