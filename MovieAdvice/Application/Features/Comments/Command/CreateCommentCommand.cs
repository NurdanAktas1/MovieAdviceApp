using Application.Features.Comments.Dtos;
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

namespace Application.Features.Comments.Command
{
    public partial class CreateCommentCommand : IRequest<CreatedCommentDto>
    {
        public string Author { get; set; }
        public int MovieId { get; set; }
        public string Content { get; set; }
       public int Point { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreatedCommentDto>
        {
            private readonly ICommentRepository _commentRepository;
            private readonly IMapper _mapper;

            public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper, MovieBusinessRules brandBusinessRules)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }

            public async Task<CreatedCommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                //await _brandBusinessRules.MovieNameCanNotBeDuplicatedWhenInserted(request.Name);

                Comment mappedComment = _mapper.Map<Comment>(request);
                Comment createdComment = await _commentRepository.AddAsync(mappedComment);
                CreatedCommentDto createdCommentDto = _mapper.Map<CreatedCommentDto>(createdComment);

                return createdCommentDto;

            }
        }
    }
}
