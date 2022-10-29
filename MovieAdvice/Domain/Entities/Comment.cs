using Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment:Entity
    {
        public string Author { get; set; }
        public int MovieId { get; set; }
     
        public string Content { get; set; }
        public DateTime?Created_at { get; set; }
        public string?CommentId { get; set; }
    public DateTime? updated_at { get; set; }
        public string url { get; set; }
        public virtual Movie? Movie { get; set; }

        public int? Point { get; set; }
        public Comment()
        {
        }

        public Comment(string author, string content, DateTime created_at, string commentId, DateTime updated_at, string url, int? point,int movieId) :this()
        {
            Author = author;
            Content = content;
            Created_at = created_at;
            CommentId = commentId;
            this.updated_at = updated_at;
            this.url = url;
            MovieId = movieId;
           Point = point;
        }

       
    }
}
