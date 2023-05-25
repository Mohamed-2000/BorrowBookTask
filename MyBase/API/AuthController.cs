using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBase.Application.Login.Commands;
using MyBase.Domain.Entities;

namespace MyBase.Web.API;
[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ApiControllersBase
{
    private IMediator _mediator;
    private readonly SignInManager<AppUser> _signInManager;
    public AuthController(IMediator mediator, SignInManager<AppUser> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.Success) return Ok(response);
        else return BadRequest(response);

    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        await HttpContext.SignOutAsync();
        return Ok();
    }
}