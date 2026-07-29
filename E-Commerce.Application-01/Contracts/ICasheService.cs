using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Contracts
{
    public interface ICasheService
    {
        Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);

        Task SetAsync(string cacheKey,object cacheValue,TimeSpan TimeToLive,CancellationToken ct = default);
    }
}
