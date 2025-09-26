using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookStore.Api.Models;
using BookStore.Application.Books.Commands;
using BookStore.Application.Books.Queries;
using MapsterMapper;


namespace BookStore.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;


    public BooksController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id), ct);
        if (book == null) return NotFound();
        var dto = _mapper.Map<BookDto>(book);
        return Ok(dto);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request, CancellationToken ct)
    {
        var command = _mapper.Map<CreateBookCommand>(request);
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
}