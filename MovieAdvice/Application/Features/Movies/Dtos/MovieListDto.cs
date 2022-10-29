using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Dtos
{
    public class MovieListDto
    {
        public bool Adult { get; set; }
        public string Backdrop_path { get; set; }
        public List<Comment> Comments { get; set; }
        public string Media_type { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public double Vote_average { get; set; }
        public int Vote_count { get; set; }
    }
}
