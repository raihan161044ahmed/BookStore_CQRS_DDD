using MediatR;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Auth.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IPasswordHasher _hasher;
    private readonly IUserRepository _repo;

    public RegisterUserCommandHandler(IPasswordHasher hasher, IUserRepository repo)
    {
        _hasher = hasher;
        _repo = repo;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if email already exists
        var existing = await _repo.GetByEmailAsync(request.Email, cancellationToken);
        if (existing != null)
            throw new InvalidOperationException("Email already registered.");

        var hash = _hasher.Hash(request.Password);
        var user = new User(Guid.NewGuid(), request.Email, hash, request.Role);
        await _repo.AddAsync(user, cancellationToken);

        return user.Id;
    }
}
