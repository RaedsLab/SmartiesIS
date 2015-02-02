using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IAdminRepository : IRepository<Admin> { }

}
