using System;
using MediatR;

namespace Core.Application.Features.Auth.Commands.Register;

public class RegisterCommandRequest: IRequest<Unit>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int? CustomerId { get; set; }
}
