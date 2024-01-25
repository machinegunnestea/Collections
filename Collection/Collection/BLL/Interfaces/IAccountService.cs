using Collection.Data;
using System.Security.Claims;

namespace Collection.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal userPrincipal, string userId = "");
    }
}