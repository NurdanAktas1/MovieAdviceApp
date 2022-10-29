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

namespace Application.Features.Movies.Commands
{
    public partial class CreateMovieCommand : IRequest<CreatedMovieDto>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateMovieCommand, CreatedMovieDto>
        {
            private readonly IMovieRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly MovieBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IMovieRepository brandRepository, IMapper mapper, MovieBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedMovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
            {
               // await _brandBusinessRules.MovieNameCanNotBeDuplicatedWhenInserted(request.Name);

                Movie mappedBrand = _mapper.Map<Movie>(request);
                Movie createdBrand = await _brandRepository.AddAsync(mappedBrand);
                CreatedMovieDto createdBrandDto = _mapper.Map<CreatedMovieDto>(createdBrand);

                return createdBrandDto;

            }
        }
    }
}
