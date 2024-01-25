using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collection.DAL.Repositories
{
    public class ImageRepository : BaseRepository<Image, ApplicationDbContext>
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}