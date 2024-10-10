using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Application.Interfaces.Tokens;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateJwtTokenAsync(string email, IList<string> roles);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
