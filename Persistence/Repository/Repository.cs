using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        /***********************************************************************************************************
           ******* Universal Entity Queries*************************************************************************
           ************************************************************************************************************/
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public async Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> Get<TEntity>(object id) where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> GetByPredicate<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : class
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HasValues<TEntity>() where TEntity : class
        {
            return await _context.Set<TEntity>().AnyAsync();
        }

        public EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Set<TEntity>().Attach(entity);
        }

    }
}