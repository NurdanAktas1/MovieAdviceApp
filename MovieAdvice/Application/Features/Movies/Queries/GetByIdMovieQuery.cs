using Application.Features.Movies.Dtos;
using Application.Features.Movies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Queries
{
    public class GetByIdMovieQuery : IRequest<MovieGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdMovieQueryHandler : IRequestHandler<GetByIdMovieQuery, MovieGetByIdDto>
        {
            private readonly IMovieRepository _movieRepository;
            private readonly IMapper _mapper;
            private readonly MovieBusinessRules _movieBusinessRules;

            public GetByIdMovieQueryHandler(IMovieRepository movieRepository, IMapper mapper, MovieBusinessRules movieBusinessRules)
            {
                _movieRepository = movieRepository;
                _mapper = mapper;
                _movieBusinessRules = movieBusinessRules;
            }

            public async Task<MovieGetByIdDto> Handle(GetByIdMovieQuery request, CancellationToken cancellationToken)
            {
                Movie? movie = await _movieRepository.GetAsync(b => b.Id == request.Id);

                _movieBusinessRules.MovieShouldExistWhenRequested(movie);

                MovieGetByIdDto movieGetByIdDto = _mapper.Map<MovieGetByIdDto>(movie);
                return movieGetByIdDto;
            }
        }
    }
}
