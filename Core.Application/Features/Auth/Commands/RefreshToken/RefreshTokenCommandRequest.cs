using System;
using MediatR;

namespace Core.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
