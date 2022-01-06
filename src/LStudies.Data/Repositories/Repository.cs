using LStudies.Business.Interfaces;
using LStudies.Business.Models;
using LStudies.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LStudies.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        /* Only inherited classes or repositories will be able to use. */
        protected readonly LStudiesDbContext dbContext;

        /* 'Shortcut' to dbset. */
        protected readonly DbSet<TEntity> dbSet;

        public Repository(LStudiesDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        /* AsNoTracking() -> Disables memory tracking, increases performace. */
        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /* Virtual enables overriding */
        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            dbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            //dbSet.Remove(await dbSet.FindAsync(id));
            dbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext?.Dispose();
        }
    }
}
