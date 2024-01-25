using Collection.BLL.Interfaces;
using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using System.Data;
using System.Security.Claims;

namespace Collection.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAdmin(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            await _unitOfWork.UserManager.AddToRoleAsync(user, "admin");
        }

        public async Task BlockUser(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user.LockoutEnabled)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = DateTimeOffset.UtcNow;
            }
            else
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(3);
            }
            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.UserManager.UpdateSecurityStampAsync(user);
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _unitOfWork.Users.Get(
                userId,
                user => user.CollectionModels,
                user => user.LikedItems,
                user => user.Comments);
            using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
            {
                await _unitOfWork.UserManager.DeleteAsync(user);
                await transaction.CommitAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers(ClaimsPrincipal claimsPrincipal)
        {
            var users = await _unitOfWork.Users.GetAll();
            var managerUsers = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var groups = await _unitOfWork.UserManager.GetRolesAsync(user);
                if (groups.Count == 0)
                {
                    managerUsers.Add(user);
                }
            }
            return managerUsers;
        }
    }
}