using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewViewModel, Review>()
                .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => DateTime.Now)) 
                .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore()) 
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
