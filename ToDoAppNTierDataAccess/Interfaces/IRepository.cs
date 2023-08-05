using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierDataAccess.Interfaces
{
    public interface IRepository<T> where  T :BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> Find(int id);
        Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false);

        Task<List<T>> GetFilteredList(Expression<Func<T, bool>> filter);

        void Remove(T entity);
        Task Create(T entity);
        void Update(T entity, T unchanged);
        bool IsThereOrNot(Expression<Func<T, bool>> filter);
        IQueryable<T> GetQuery();

    }
}
