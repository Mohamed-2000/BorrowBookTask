using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBase.Application.Books.Commands;
using MyBase.Application.Books.Queries;
using MyBase.Application.BorrowedBook.Commands;

namespace MyBase.Web.API;
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BookController : ApiControllersBase
{

    private IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public BookController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _env = environment;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBooksCommand command)
    {
        command.UserId = CurrentUserId;
        command.WebRootPath = _env.WebRootPath;
        var response = await _mediator.Send(command);
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksQuery model)
    {

        var response = await _mediator.Send(model);
        if (response.data.Any()) return Ok(response);
        else return BadRequest(response);
    }


    public async Task<IActionResult> BorrowBookForCustomer([FromForm] CreateBorrowedBooksCommand command)
    {
        command.UserId = CurrentUserId;
        
        var response = await _mediator.Send(command);
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }

}