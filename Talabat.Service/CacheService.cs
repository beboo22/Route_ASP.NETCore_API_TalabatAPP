using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.ServiceContract;

namespace Talabat.Service
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        public CacheService(IConnectionMultiplexer rdx)
        {
            _db = rdx.GetDatabase();
        }
        public async Task CreateCache(string CacheKey, object Response, TimeSpan time)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var response = JsonSerializer.Serialize(Response, option);

            await _db.StringSetAsync(CacheKey, response,time);

            //var res = 
            //return res;
        }


        public async Task<string?> GetFromCacheAsync(string CacheKey)
        {
            var item = await _db.StringGetAsync(CacheKey);
            if (item.HasValue)
            {
                return item.ToString();
            }
            return null;
        }




    }
}
