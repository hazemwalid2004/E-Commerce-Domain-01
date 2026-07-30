using E_Commerce.Application_01.Common;
using E_Commerce.Application_01.DTOS.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);

        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default);
    }
}
