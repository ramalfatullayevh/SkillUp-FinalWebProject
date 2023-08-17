using SkillUp.Core.Entities;
using System.Linq.Expressions;

namespace SkillUp.DAL.Repositories.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        //Get All
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,params Expression<Func<T, object>>[] includeProperties);

        //Get
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        //GetById
        T GetByIdAsync(int id); 

        //Add
        Task AddAsync(T entity);

        //Delete
        Task DeleteAsync(int id); 

        //Update
        Task<T> UpdateAsync(T entity);
    }
}
