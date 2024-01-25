using AutoMapper;
using Collection.BLL.DTO;
using Collection.DAL.Entities;

namespace Collection.BLL.Profiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<CollectionModel, CollectionDTO>();
        }
    }
}