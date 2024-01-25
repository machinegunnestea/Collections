using AutoMapper;
using Collection.BLL.DTO;
using Collection.Data;
using Microsoft.AspNetCore.Identity;

namespace Collection.BLL.Profiles.MapperExtensions
{
    public static class UserProfileExtension
    {
        public static AccountDTO MapUserToDto(this IMapper mapper, UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var userDTO = mapper.Map<AccountDTO>(user);
            userDTO.Roles = userManager.GetRolesAsync(user).Result;
            return userDTO;
        }

        public static IEnumerable<AccountDTO> MapUsersToDto(this IMapper mapper, UserManager<ApplicationUser> userManager, IEnumerable<ApplicationUser> users)
        {
            var usersDTO = users
                .Select(user =>
                {
                    var userDTO = mapper.Map<AccountDTO>(user);
                    userDTO.Roles = userManager.GetRolesAsync(user).Result;
                    return userDTO;
                });
            return usersDTO;
        }
    }
}