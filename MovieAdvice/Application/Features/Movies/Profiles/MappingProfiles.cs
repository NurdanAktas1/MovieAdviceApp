
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using AutoMapper;
using Core.Persistance.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Movie, CreatedMovieDto>().ReverseMap();
           // CreateMap<Movie, CreateMovieCommand>().ReverseMap();
            CreateMap<IPaginate<Movie>, MovieListModel>().ReverseMap();
            CreateMap<Movie, MovieListDto>().ReverseMap();
            CreateMap<Movie, MovieGetByIdDto>().ReverseMap();
        }
    }
}
