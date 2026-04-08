using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        { 
            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                    src.Actors != null
                    ? src.Actors.Select(ma => ma.Actor).Take(6).ToList()
                    : new List<Actor>()
                )).ReverseMap();

            CreateMap<Movie, UpdateMovieViewModel>()
            .ForMember(dest => dest.ExistingImageName, opt => opt.MapFrom(src => src.Image_Name))
            .ForMember(dest => dest.ImageFile, opt => opt.Ignore());

            CreateMap<UpdateMovieViewModel, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Image_Name, opt => opt.Ignore())
                .ForMember(dest => dest.MovieGenres, opt => opt.Ignore())
                .ForMember(dest => dest.Actors, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore());
        }
    }
}
