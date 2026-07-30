using E_Commerce.Application_01.Common;
using E_Commerce.Application_01.DTOS.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Contracts
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);
    }
}
