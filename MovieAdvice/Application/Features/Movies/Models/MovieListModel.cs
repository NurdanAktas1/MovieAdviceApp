using Application.Features.Movies.Dtos;
using Core.Persistance.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Models
{
    public class MovieListModel : BasePageableModel
    {
        public IList<MovieListDto> Items { get; set; }

       
    }
}
