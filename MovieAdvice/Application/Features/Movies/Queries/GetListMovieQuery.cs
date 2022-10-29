using Application.Features.Movies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Queries
{
    public class GetListMovieQuery : IRequest<MovieListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListMovieQueryHandler : IRequestHandler<GetListMovieQuery, MovieListModel>
        {
            private readonly IMovieRepository _movieRepository;
            private readonly IMapper _mapper;

            public GetListMovieQueryHandler(IMovieRepository movieRepository, IMapper mapper)
            {
                _movieRepository = movieRepository;
                _mapper = mapper;
            }

            public async Task<MovieListModel> Handle(GetListMovieQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Movie> movies = await _movieRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                MovieListModel mappedMovieListModel = _mapper.Map<MovieListModel>(movies);

                return mappedMovieListModel;
            }
        }
    }
}
