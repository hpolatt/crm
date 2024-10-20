using System;

namespace Core.Application.Features.Auth.Commands.Login;

public class LoginCommandResponse
{
    public string Token { get; }
    public string RefreshToken { get; }
    public DateTime Expiration { get; }

    public LoginCommandResponse(string token, string refreshToken, DateTime expiration){
        Token = token;
        RefreshToken = refreshToken;
        Expiration = expiration;
    }
}
