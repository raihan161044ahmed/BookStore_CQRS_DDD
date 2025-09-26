using MediatR;


namespace BookStore.Application.Auth.Commands;


public record RegisterUserCommand(string Email, string Password, string? Role = null) : IRequest<Guid>;