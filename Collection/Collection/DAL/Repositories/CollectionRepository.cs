using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public class CollectionRepository : BaseRepository<CollectionModel, ApplicationDbContext>
    {
        public CollectionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}