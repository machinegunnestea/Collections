using Collection.Data;
using System.Security.Claims;

namespace Collection.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetUsers(ClaimsPrincipal claimsPrincipal);

        Task AddAdmin(string userId);

        Task BlockUser(string userId);

        Task DeleteUser(string userId);
    }
}