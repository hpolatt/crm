using System;
using Core.Application.Base;

namespace Core.Application.Features.Auth.Exceptions;

public class UserAlreadyExistException :BaseException
{
    public UserAlreadyExistException() : base("User Already Exist!") {}
}
