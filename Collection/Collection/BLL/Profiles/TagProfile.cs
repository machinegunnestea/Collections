using AutoMapper;
using Collection.BLL.DTO;
using Collection.DAL.Entities;

namespace Collection.BLL.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDTO>().ReverseMap();
        }
    }
}