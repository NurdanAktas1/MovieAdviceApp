using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.Dtos
{
    public class CreatedCommentDto
    {
        public string Author { get; set; }
        public int MovieId { get; set; }

        public string Content { get; set; }
        public DateTime Created_at { get; set; }
        public string CommentId { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public decimal Point { get; set; }
    }
}
