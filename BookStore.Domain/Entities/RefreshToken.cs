using System;


namespace BookStore.Domain.Entities;


public sealed class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Revoked { get; set; }
    public string? CreatedByIp { get; set; }
    public string? ReplacedByToken { get; set; }


    public RefreshToken() { }


    public RefreshToken(Guid id, Guid userId, string token, DateTime expires, DateTime created, string? createdByIp = null)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        UserId = userId;
        Token = !string.IsNullOrWhiteSpace(token) ? token : throw new ArgumentException("Token is required", nameof(token));
        Expires = expires;
        Created = created;
        CreatedByIp = createdByIp;
    }


    public bool IsActive => Revoked == null && DateTime.UtcNow < Expires;


    public void Revoke(DateTime revokedAt, string? replacedByToken = null)
    {
        if (Revoked != null) return;
        Revoked = revokedAt;
        ReplacedByToken = replacedByToken;
    }
}

