using MediatR;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;


namespace BookStore.Application.Books.Queries;


public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book?>
{
    private readonly IBookRepository _repo;


    public GetBookByIdQueryHandler(IBookRepository repo)
    {
        _repo = repo;
    }


    public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdAsync(request.Id, cancellationToken);
    }
}