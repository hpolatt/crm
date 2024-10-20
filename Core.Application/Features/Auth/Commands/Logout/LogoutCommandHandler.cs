using System;
using Core.Application.Base;
using Core.Application.Features.Auth.Rules;
using Core.Application.Interfaces.AutoMapper;
using Core.Application.Interfaces.UnitOfWorks;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler : BaseHandler, IRequestHandler<LogoutCommandRequest, Unit>
{
    private readonly UserManager<User> userManager;
    private readonly AuthRules authRules;

    public LogoutCommandHandler(UserManager<User> userManager, AuthRules authRules, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor) {
        this.userManager = userManager;
        this.authRules = authRules;
    }
    public async Task<Unit> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByIdAsync(userId);
        await authRules.EmailAddressShouldBeExist(user);
        user.RefreshToken = null;
        await userManager.UpdateAsync(user);

        return Unit.Value;
    }
}
