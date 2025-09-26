using BCrypt.Net;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace BookStore.Infrastructure.Auth;


public class PasswordHasherAdapter : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }


    public bool Verify(string password, string hashed)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashed);
    }
}