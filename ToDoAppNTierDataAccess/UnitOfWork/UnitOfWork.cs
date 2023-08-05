using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDataAccess.Contexts;
using ToDoAppNTierDataAccess.Interfaces;
using ToDoAppNTierDataAccess.Repositories;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierDataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoContext _context;

        public UnitOfWork(ToDoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The Unit of Work is a type of business transaction, and it will aggregate
        /// all Repository transactions (CRUD) into a single transaction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        /// <summary>
        /// Changes made are reflected in the database with this method.
        /// Saving changes is out of control of repositories.
        /// </summary>
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
