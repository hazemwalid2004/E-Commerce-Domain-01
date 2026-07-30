using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Contracts
{
    public interface ITokenService
    {
        string CreateToken(string userId, string email, string userName, IEnumerable<string> roles);
    }
}
