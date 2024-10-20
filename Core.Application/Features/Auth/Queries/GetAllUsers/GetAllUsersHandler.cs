using System;
using System.Security.Cryptography.X509Certificates;
using Core.Application.Base;
using Core.Application.Interfaces.AutoMapper;
using Core.Application.Interfaces.UnitOfWorks;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Auth.Queries.GetAllUsers;

public class GetAllUsersHandler : BaseHandler, IRequestHandler<GetAllUsersRequest, IList<GetAllUsersResponse>>
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;

    public GetAllUsersHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor) {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public Task<IList<GetAllUsersResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        // Roles Claims
         var users = userManager.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ToList();
    
        IList<GetAllUsersResponse> results = users.Select(x => new GetAllUsersResponse() {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                CustomerName = x.Customer?.Name,
                RoleName = x.UserRoles.Select(x => x.Role.Name).FirstOrDefault(),
                Permissions = x.UserRoles.Select(x => x.Role.Permissions).FirstOrDefault()
            }).ToList();

        return Task.FromResult(results);

    }
}
