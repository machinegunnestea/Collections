using Collection.DAL.Entities;
using Collection.Data;
using Microsoft.AspNetCore.Identity;

namespace Collection.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext Context { get; }
        IBaseInterface<CollectionModel> Collections { get; }
        IBaseInterface<Comment> Comments { get; }
        IBaseInterface<Image> Images { get; }
        IBaseInterface<Item> Items { get; }
        IBaseInterface<Tag> Tags { get; }
        IUserInterface<ApplicationUser> Users { get; }
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<ApplicationUser> SignInManager { get; }

        Task SaveAsync();
    }
}