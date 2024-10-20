using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Core.Application.Base;
using Core.Application.Features.Auth.Rules;
using Core.Application.Interfaces.AutoMapper;
using Core.Application.Interfaces.Tokens;
using Core.Application.Interfaces.UnitOfWorks;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{

    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;
    private readonly ITokenService tokenService;
    private readonly RoleManager<Role> roleManager;
    private readonly AuthRules authRules;

    public LoginCommandHandler(UserManager<User> userManager, IConfiguration configuration, ITokenService tokenService, RoleManager<Role> roleManager, AuthRules authRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.tokenService = tokenService;
        this.roleManager = roleManager;
        this.authRules = authRules;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByNameAsync(request.Email);
        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        await authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

        IList<string> roles = await userManager.GetRolesAsync(user);

        JwtSecurityToken jwtSecurityToken = await tokenService.GenerateJwtTokenAsync(user, roles);
        user.RefreshToken = tokenService.GenerateRefreshToken();

        int.TryParse(configuration["Jwt:RefreshTokenExpirationInDays"], out int refreshTokenExpirationInDays);

        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenExpirationInDays);

        var x = await userManager.UpdateAsync(user);
        await userManager.UpdateSecurityStampAsync(user);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        await userManager.SetAuthenticationTokenAsync(user, "Bearer", "AccessToken", token);

        return new LoginCommandResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime.Value);
    }

}
