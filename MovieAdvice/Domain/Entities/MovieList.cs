using Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovieList : Entity
    {
        public string Name { get; set; }
        public string Created_by { get; set; }
        public string Description { get; set; }
        public int Favorite_count { get; set; }
        public virtual ICollection<Movie> items { get; set; }
        public int Item_count { get; set; }
        public string Poster_path { get; set; }
        public double Average_rating { get; set; }
        public string Backdrop_path { get; set; }
        public long Revenue { get; set; }
        public int Runtime { get; set; }

    }


}
