using Application.Services.Repositories;
using Core.Persistance.Repositories;
using Domain.Entities;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class MovieRepository : EfRepositoryBase<Movie, BaseDbContext>, IMovieRepository
    {
        public MovieRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
