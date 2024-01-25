using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public class CommentRepository : BaseRepository<Comment, ApplicationDbContext>
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}