using MediatR;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;


namespace BookStore.Application.Books.Commands;


public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _repo;


    public CreateBookCommandHandler(IBookRepository repo)
    {
        _repo = repo;
    }


    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(Guid.NewGuid(), request.Title, request.Price, request.Stock, request.AuthorId, request.ISBN, request.Description, request.PublishedAt);
        await _repo.AddAsync(book, cancellationToken);
        return book.Id;
    }
}