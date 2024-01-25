using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public class TagRepository : BaseRepository<Tag, ApplicationDbContext>
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}