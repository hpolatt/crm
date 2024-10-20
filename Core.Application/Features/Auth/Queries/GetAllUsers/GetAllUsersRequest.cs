using System;
using MediatR;

namespace Core.Application.Features.Auth.Queries.GetAllUsers;

public class GetAllUsersRequest: IRequest<IList<GetAllUsersResponse>>
{

}
