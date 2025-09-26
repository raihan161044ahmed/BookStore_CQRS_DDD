using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Persistence;


namespace BookStore.Infrastructure.Repositories;


public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly BookStoreDbContext _db;


    public RefreshTokenRepository(BookStoreDbContext db)
    {
        _db = db;
    }


    public async Task AddAsync(RefreshToken token, CancellationToken ct = default)
    {
        await _db.RefreshTokens.AddAsync(token, ct);
    }


    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
    {
        return await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token, ct);
    }


    public void Update(RefreshToken token)
    {
        _db.RefreshTokens.Update(token);
    }


    public void Remove(RefreshToken token)
    {
        _db.RefreshTokens.Remove(token);
    }
}