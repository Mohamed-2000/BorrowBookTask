using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBase.Web.API;
[Route("api/[controller]/[action]")]
[ApiController]
public class FineController : ApiControllersBase
{

    private IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public FineController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _env = environment;
    }

}
