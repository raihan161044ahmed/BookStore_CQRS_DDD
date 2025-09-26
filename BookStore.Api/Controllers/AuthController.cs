using Microsoft.AspNetCore.Mvc;
using MediatR;
using BookStore.Application.Auth.Commands;


namespace BookStore.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;


    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);
        return Ok(new { id });
    }
}