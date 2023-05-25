using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBase.Application.Books.Queries;

namespace MyBase.Web.API;
[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class CustomerController  : ApiControllersBase
{

    private IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public CustomerController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _env = environment;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksQuery model)
    {

        var response = await _mediator.Send(model);
        if (response.data.Any()) return Ok(response);
        else return BadRequest(response);
    }
}
