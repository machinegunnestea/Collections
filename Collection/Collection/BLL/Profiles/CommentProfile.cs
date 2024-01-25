using AutoMapper;
using Collection.BLL.DTO;
using Collection.DAL.Entities;

namespace Collection.BLL.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}