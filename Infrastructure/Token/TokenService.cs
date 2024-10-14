using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Application.Interfaces.Tokens;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Token;

public class TokenService : ITokenService
{
    private readonly TokenSettings tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        this.tokenSettings = tokenSettings.Value;
    }

    public async Task<JwtSecurityToken> GenerateJwtTokenAsync(User user, IList<string> roles)
    {
        var Claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        foreach (var role in roles)
        {
            Claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));
        var token = new JwtSecurityToken(issuer: tokenSettings.Issuer,
            audience: tokenSettings.Audience,
            claims: Claims,
            expires: DateTime.UtcNow.AddMinutes(tokenSettings.TokenValidtyInMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
