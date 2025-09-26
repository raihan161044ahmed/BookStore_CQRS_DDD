using MediatR;
using BookStore.Domain.Entities;


namespace BookStore.Application.Books.Queries;


public record GetBookByIdQuery(Guid Id) : IRequest<Book?>;