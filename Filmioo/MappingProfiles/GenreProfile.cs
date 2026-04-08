using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreViewModel>().ReverseMap();
        }
    }
}
