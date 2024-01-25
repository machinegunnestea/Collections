using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Collection.DAL.Interfaces
{
    public interface IBaseInterface<T> where T : class
    {
        Task<T> Get(int id, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);

        T Add(T item);

        T Update(T item);

        Task<T> Delete(int id);
    }
}