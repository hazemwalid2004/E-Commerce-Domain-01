using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ICasheRepository
    {
        Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);

        Task SetAsync( string cacheKey,string cacheValue,TimeSpan TimeToLive,CancellationToken ct = default);
    }
}
