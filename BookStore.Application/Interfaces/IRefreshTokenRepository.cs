using BookStore.Domain.Entities;


namespace BookStore.Application.Interfaces;


public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token, CancellationToken ct = default);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);
    void Update(RefreshToken token);
    void Remove(RefreshToken token);
}