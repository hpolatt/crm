using System;
using Core.Application.Base;
using Core.Application.Features.Auth.Exceptions;
using Core.Domain.Entities;

namespace Core.Application.Features.Auth.Rules;

public class AuthRules : BaseRule
{
    public Task UserShouldNotBeExist(User? user) {
        if (user is not null) throw new UserAlreadyExistException();
        return Task.CompletedTask;
    }

    public Task EmailOrPasswordShouldNotBeInvalid(User? user, bool checkPassword)
    {
        if (user is null || !checkPassword) throw new InvalidUserEmailOrPasswordException();

        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldNotBeExpired(DateTime? refreshTokenExpiryTime)
    {
        if (refreshTokenExpiryTime <= DateTime.Now) throw new RefreshTokenExpiredException();

        return Task.CompletedTask;
    }

    public Task EmailAddressShouldBeExist(User? user)
    {
        if (user is null) throw new EmailAddressShouldBeExistException();

        return Task.CompletedTask;
    }
}
