using BookStore.Domain.Entities;


namespace BookStore.Application.Interfaces;


public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    RefreshToken CreateRefreshToken(string createdByIp);
}