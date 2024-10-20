using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Application.Base;
using Core.Application.Features.Auth.Rules;
using Core.Application.Interfaces.AutoMapper;
using Core.Application.Interfaces.Tokens;
using Core.Application.Interfaces.UnitOfWorks;
using Core.Domain.Common;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly ITokenService tokenService;
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;
    private readonly RoleManager<Role> roleManager;
    private readonly AuthRules authRules;

    public RefreshTokenCommandHandler(AuthRules authRules,ITokenService tokenService, UserManager<User> userManager, IConfiguration configuration, RoleManager<Role> roleManager, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor) {
        this.tokenService = tokenService;
        this.userManager = userManager;
        this.configuration = configuration;
        this.roleManager = roleManager;
        this.authRules = authRules;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal claimsPrincipal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        User user = await userManager.FindByEmailAsync(email);
        IList<string> roles = await userManager.GetRolesAsync(user);

        await authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);

        JwtSecurityToken jwtSecurityToken = await tokenService.GenerateJwtTokenAsync(user, roles);
        user.RefreshToken = tokenService.GenerateRefreshToken();
        await userManager.UpdateAsync(user);

        return new RefreshTokenCommandResponse(){
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = user.RefreshToken,
        };
    }
}
