using Application.Features.Comments.Command;
using Application.Features.Comments.Dtos;
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

namespace Application.Features.Comments.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Comment, CreatedCommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentCommand>().ReverseMap();
        }
    }
}
