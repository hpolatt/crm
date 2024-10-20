using System;
using Core.Application.Base;

namespace Core.Application.Features.Auth.Exceptions;

public class InvalidUserEmailOrPasswordException : BaseException
{
    public InvalidUserEmailOrPasswordException() : base("Invalid Email or Password!") {}

}
