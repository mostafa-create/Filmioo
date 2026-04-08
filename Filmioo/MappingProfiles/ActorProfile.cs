using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorViewModel>().ReverseMap();

            CreateMap<Actor, UpdateActorViewModel>()
                .ForMember(dest => dest.ExistingImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl))
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore());

            CreateMap<UpdateActorViewModel, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());
        }
    }
}
