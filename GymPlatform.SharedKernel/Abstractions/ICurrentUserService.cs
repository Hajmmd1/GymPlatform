using System;
using System.Collections.Generic;

namespace GymPlatform.SharedKernel;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    Guid? TenantId { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    IEnumerable<string> Permissions { get; }
}