using Collection.DAL.Entities;
using Collection.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public class ItemRepository : BaseRepository<Item, ApplicationDbContext>
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}