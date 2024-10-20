using System;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class UserToken: IdentityUserToken<Guid>
{

}
