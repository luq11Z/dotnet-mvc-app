using LStudies.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LStudies.Business.Interfaces
{
    /* Provides all necessary methods to any entity.
       Obligates the repository do dispose (to free up memory).
       TEntity can only be applied to Entity childs. This way it is not possible passing any type of objects.
       In other words, only objects that implements Entity can use this interface.
     */
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        /* Tasks makes better usability, performace and helth to the system. */
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(Guid id);
        /* Find by linq/lambda expression*/
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
