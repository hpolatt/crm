using System;
using System.Security.Claims;
using Core.Application.Interfaces.AutoMapper;
using Core.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Base;

public class BaseHandler
{
  protected readonly IMapper mapper;
    protected readonly IUnitOfWork unitOfWork;
    protected readonly IHttpContextAccessor httpContextAccessor;
    protected readonly string? userId;
    public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.httpContextAccessor = httpContextAccessor;
        this.userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
