using AutoMapper;
using Collection.BLL.DTO;
using Collection.DAL.Entities;

namespace Collection.BLL.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDTO>().ReverseMap();
        }
    }
}