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
    public class CommentRepository : EfRepositoryBase<Comment, BaseDbContext>, ICommentRepository
    {
        public CommentRepository(BaseDbContext context) : base(context)
        {
        }

    }
}
