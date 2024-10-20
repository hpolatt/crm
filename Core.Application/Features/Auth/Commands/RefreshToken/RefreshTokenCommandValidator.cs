using System;
using FluentValidation;

namespace Core.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidator() {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access Token is required");
        
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token is required");
    }
}
