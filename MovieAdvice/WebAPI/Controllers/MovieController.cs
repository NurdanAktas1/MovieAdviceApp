using Application.Features.Comments.Command;
using Application.Features.Comments.Dtos;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using Application.Features.Movies.Queries;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListMovieQuery getListMovieQuery = new() { PageRequest = pageRequest };
            MovieListModel result = await Mediator.Send(getListMovieQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdMovieQuery getByIdIdMovieQuery)
        {
            MovieGetByIdDto movieGetByIdDto = await Mediator.Send(getByIdIdMovieQuery);
            return Ok(movieGetByIdDto);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCommentCommand createCommentCommand)
        {
            CreatedCommentDto result = await Mediator.Send(createCommentCommand);
            return Created("", result);
        }

        
    }
}
