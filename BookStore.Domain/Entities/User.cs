using System;
using System.Collections.Generic;


namespace BookStore.Domain.Entities;


public sealed class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? Role { get; private set; }


    // Navigation
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();


    private User() { }


    public User(Guid id, string email, string passwordHash, string? role = null)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email is required", nameof(email));
        PasswordHash = !string.IsNullOrWhiteSpace(passwordHash) ? passwordHash : throw new ArgumentException("PasswordHash is required", nameof(passwordHash));
        Role = role;
    }


    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required", nameof(passwordHash));
        PasswordHash = passwordHash;
    }


    public void SetRole(string role)
    {
        Role = role;
    }


    public void AddRefreshToken(RefreshToken token)
    {
        if (token == null) throw new ArgumentNullException(nameof(token));
        RefreshTokens.Add(token);
    }
}