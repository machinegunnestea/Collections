using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public class UserRepository : IUserInterface<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Add(ApplicationUser entity)
        {
            _context.Set<ApplicationUser>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ApplicationUser> Delete(string id)
        {
            var entity = await _context.Set<ApplicationUser>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<ApplicationUser>().Remove(entity);
            }
            return entity;
        }

        public IEnumerable<ApplicationUser> Find(Func<ApplicationUser, bool> predicate, params Expression<Func<ApplicationUser, object>>[] includes)
        {
            return _context.Set<ApplicationUser>()
                .IncludeMultiple(includes)
                .Where(predicate)
                .ToList();
        }

        public async Task<ApplicationUser> Get(string id, params Expression<Func<ApplicationUser, object>>[] includes)
        {
            return await _context.Set<ApplicationUser>()
                           .IncludeMultiple(includes)
                           .SingleOrDefaultAsync(entity => EF.Property<string>(entity, "Id").Equals(id));
        }

        public async Task<ApplicationUser> Update(ApplicationUser item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll(params Expression<Func<ApplicationUser, object>>[] includes)
        {
            return await _context.Set<ApplicationUser>()
                .IncludeMultiple(includes)
                .ToListAsync();
        }
    }
}