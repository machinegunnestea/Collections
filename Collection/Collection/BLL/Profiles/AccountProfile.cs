using AutoMapper;
using Collection.BLL.DTO;
using Collection.Data;

namespace Collection.BLL.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, AccountDTO>();
        }
    }
}