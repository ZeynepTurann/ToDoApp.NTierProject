using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDataAccess.Interfaces;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierDataAccess.UnitOfWork
{
     public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChanges();
    }
}

