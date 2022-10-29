using Application.Services.Repositories;
using Core.CrossCuttingConcerns;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Rules
{
    public class MovieBusinessRules
    {
        private readonly IMovieRepository _movieRepository;

        public MovieBusinessRules(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void MovieShouldExistWhenRequested(Movie movie)
        {
            if (movie == null) throw new BusinessException("Requested movie does not exist");
        }
    }
}
