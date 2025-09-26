using MediatR;
using BookStore.Domain.Entities;


namespace BookStore.Application.Books.Commands;


public record CreateBookCommand(string Title, decimal Price, int Stock, Guid? AuthorId = null, string? ISBN = null, string? Description = null, DateTime? PublishedAt = null) : IRequest<Guid>;