using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDataAccess.Contexts;
using ToDoAppNTierDataAccess.Interfaces;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierDataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ToDoContext _context;

        public Repository(ToDoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The data that comes as a parameter is added to the database.
        /// </summary>
        /// <param name="entity"></param>
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <summary>
        ///Check entity with int id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return T entity if it exists</returns>
        public async Task<T> Find(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        ///  Get all entities which are type of T
        ///  incoming list is for reading only(asNoTracking)
        /// </summary>
        /// <returns> return all entities</returns>
        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Check entity with expression argument and tracking situation
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <returns>return T entity if it exists</returns>
         public async Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? await _context.Set<T>().SingleOrDefaultAsync(filter) : await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        /// <summary>
        /// Check entities with expression argument
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>return List of T entities if it exists</returns>
        public async Task<List<T>> GetFilteredList(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().AsNoTracking().Where(filter).ToListAsync();
        }

        /// <summary>
        /// Get all entities 
        /// </summary>
        /// <returns>return list of all entities as queryable </returns>
        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public bool IsThereOrNot(Expression<Func<T, bool>> filter)
        {
          return  GetQuery().Any(filter);
        }


        /// <summary>
        ///  The data that comes as a parameter is removed from the database
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// (property-based update)
        /// The properties of the entity that we want to change are set with new values.
        /// </summary>
        /// <param name="unchanged"></param>
        /// <param name="entity"></param>

        public void Update(T entity,T unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(entity);
        }
    }
}
