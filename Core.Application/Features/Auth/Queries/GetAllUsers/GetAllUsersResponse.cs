using System;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Application.Features.Auth.Queries.GetAllUsers;

public class GetAllUsersResponse
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? RoleName { get; set; }
    public List<Permission>? Permissions { get; set; }
}
