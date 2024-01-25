using Collection.BLL.Interfaces;
using Collection.DAL.Interfaces;
using Collection.Data;
using System.Security.Claims;

namespace Collection.BLL.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork Database { get; set; }

        public AccountService(IUnitOfWork database)
        {
            Database = database;
        }

        public async Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal userPrincipal, string userId = "")
        {
            if (!string.IsNullOrEmpty(userId) && userPrincipal.IsInRole("admin"))
            {
                return await Database.UserManager.FindByIdAsync(userId);
            }
            return await Database.UserManager.GetUserAsync(userPrincipal);
        }
    }
}