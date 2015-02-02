using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class AdRepository : RepositoryBase<Ad>, IAdRepository
    {
        public AdRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IAdRepository : IRepository<Ad> { }

}
