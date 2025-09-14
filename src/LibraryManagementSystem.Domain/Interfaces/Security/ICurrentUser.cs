using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Interfaces.Security
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        IEnumerable<string> Roles { get; }
        bool IsAuthenticated { get; }
    }
}
