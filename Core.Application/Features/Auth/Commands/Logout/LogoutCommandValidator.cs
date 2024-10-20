using System;
using FluentValidation;

namespace Core.Application.Features.Auth.Commands.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommandRequest>
{
    public LogoutCommandValidator(){
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access Token is required");
    }

}
