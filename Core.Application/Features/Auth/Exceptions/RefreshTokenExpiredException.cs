using System;
using Core.Application.Base;

namespace Core.Application.Features.Auth.Exceptions;

public class RefreshTokenExpiredException: BaseException
{
    public RefreshTokenExpiredException() : base("Refresh Token is expired!") {}
}
