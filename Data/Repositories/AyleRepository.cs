﻿using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class AyleRepository : RepositoryBase<Ayle>, IAyleRepository
    {
        public AyleRepository(DatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    public interface IAyleRepository : IRepository<Ayle> { }

}
