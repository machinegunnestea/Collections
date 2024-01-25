using Collection.Data;
using System.Linq.Expressions;

namespace Collection.DAL.Interfaces
{
    public interface IUserInterface<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAll(params Expression<Func<ApplicationUser, object>>[] includes);

        Task<ApplicationUser> Get(string id, params Expression<Func<ApplicationUser, object>>[] includes);

        IEnumerable<ApplicationUser> Find(Func<ApplicationUser, bool> predicate, params Expression<Func<ApplicationUser, object>>[] includes);

        Task<ApplicationUser> Add(ApplicationUser item);

        Task<ApplicationUser> Update(ApplicationUser item);

        Task<ApplicationUser> Delete(string id);
    }
}