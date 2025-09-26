using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BookStoreDbContext _db;

    public UserRepository(BookStoreDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _db.Users.AddAsync(user, ct);
        await _db.SaveChangesAsync(ct);
    }

    public void Update(User user)
    {
        _db.Users.Update(user);
        _db.SaveChanges();
    }

    public void Remove(User user)
    {
        _db.Users.Remove(user);
        _db.SaveChanges();
    }
}
