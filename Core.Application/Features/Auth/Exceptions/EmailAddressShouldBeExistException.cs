using System;
using Core.Application.Base;

namespace Core.Application.Features.Auth.Exceptions;

public class EmailAddressShouldBeExistException : BaseException
{
    public EmailAddressShouldBeExistException() : base("Email Address should be exist!") {}
}
