using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public abstract class BaseRepository<T, TContext> : IBaseInterface<T> where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
            return entity;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            return _context.Set<T>()
                .IncludeMultiple(includes)
                .Where(predicate)
                .ToList();
        }

        public async Task<T> Get(int id, params Expression<Func<T, object>>[] includes)
        {
            return await _context.Set<T>()
                           .IncludeMultiple(includes)
                           .SingleOrDefaultAsync(entity => EF.Property<int>(entity, "Id").Equals(id));
        }

        public T Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }
    }
}