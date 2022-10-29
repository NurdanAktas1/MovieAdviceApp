using Application.Features.Comments.Command;
using Application.Features.Comments.Dtos;
using Application.Features.Movies.Commands;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using Application.Features.Movies.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCommentCommand createCommentCommand)
        {
            CreatedCommentDto result = await Mediator.Send(createCommentCommand);
            return Created("", result);
        }
    }
}
