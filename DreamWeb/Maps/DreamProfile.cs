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
                .ForMember(p => p.Content, p => p.MapFrom(src => DreamContentConverter.Concatenate(src.Content)))
                .ForMember(p => p.CreationDate, p => p.MapFrom(src => DateTime.Now.Date));
        }
    }
}
