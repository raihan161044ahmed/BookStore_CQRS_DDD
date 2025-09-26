using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Settings;
using System.Security.Cryptography;


namespace BookStore.Infrastructure.Auth;


public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;


    public JwtTokenService(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }


    public string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_settings.Secret);


        var claims = new List<Claim>
{
new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
new Claim(JwtRegisteredClaimNames.Email, user.Email),
new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
};


        if (!string.IsNullOrEmpty(user.Role))
            claims.Add(new Claim(ClaimTypes.Role, user.Role));


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };


        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public RefreshToken CreateRefreshToken(string createdByIp)
    {
        // create cryptographically secure random token
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(randomBytes);


        return new RefreshToken
        {
            Token = token,
            Expires = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
            Created = DateTime.UtcNow,
            CreatedByIp = createdByIp
        };
    }
}