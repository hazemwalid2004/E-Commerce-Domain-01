using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure_01.Repositories
{
    public class CasheRepository : ICasheRepository
    {
        private readonly IDatabase _database;

        public CasheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string casheKey, CancellationToken ct = default)
        {
            var value = await _database.StringGetAsync(casheKey);

            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public Task SetAsync(string casheKey,string casheValue,TimeSpan TimeToLive,CancellationToken ct = default)
         => _database.StringSetAsync(casheKey, casheValue, TimeToLive);
    }
}
