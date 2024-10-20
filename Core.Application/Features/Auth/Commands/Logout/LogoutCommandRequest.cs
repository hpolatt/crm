using System;
using MediatR;

namespace Core.Application.Features.Auth.Commands.Logout;

public class LogoutCommandRequest: IRequest<Unit>
{
    public string AccessToken { get; set; }
}
