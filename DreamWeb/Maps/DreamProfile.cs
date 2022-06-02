using AutoMapper;
using DreamWeb.DAL.Entities;
using DreamWeb.Models;

namespace DreamWeb.Maps
{
    public class DreamProfile : Profile
    {
        public DreamProfile()
        {
            CreateMap<DreamInputModel, Dream>()
                .ForMember( p => p.Content, p => p.MapFrom(src => DreamContentConverter.Convert(src.Content)));
        }
    }
}
