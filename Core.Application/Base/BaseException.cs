using System;

namespace Core.Application.Base;

public class BaseException : ApplicationException
{
    public BaseException() {}

    public BaseException(string message) : base(message) {}
}
