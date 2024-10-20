using System;
using Core.Application.Base;
using Core.Application.Features.Auth.Rules;
using Core.Application.Interfaces.AutoMapper;
using Core.Application.Interfaces.UnitOfWorks;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
{
    private AuthRules authRules;
    private UserManager<User> userManager;
    private RoleManager<Role> roleManager;

    public RegisterCommandHandler(AuthRules authRules, UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor) {
        this.authRules = authRules;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }


    public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));

        User user = mapper.Map<User, RegisterCommandRequest>(request);

        user.UserName = request.Email;
        user.NormalizedEmail = request.Email.ToUpper();
        user.NormalizedUserName = request.Email.ToUpper();
        user.SecurityStamp = Guid.NewGuid().ToString();

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new Exception("User creation failed!");

        var role = await roleManager.RoleExistsAsync("user");

        if (!role) {
            await roleManager.CreateAsync(new Role { 
                Name = "user",  
                Permissions = new List<Permission> {
                    Permission.ViewCustomers,
                    Permission.ViewOrders
                }
                });
        }
        await userManager.AddToRoleAsync(user, "user");
        return Unit.Value;
    }
}
