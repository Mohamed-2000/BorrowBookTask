using Microsoft.Extensions.Caching.Distributed;
using MyBase.Application.Interfaces;
using Newtonsoft.Json;

namespace MyBase.Web.Services
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _cache;
        private readonly string PERMISSIONSKEY;
        private readonly IConfiguration _cfg;

        public RedisCache(IDistributedCache cache, IConfiguration cfg)
        {
            _cache = cache;
            _cfg = cfg;
            PERMISSIONSKEY = _cfg["redis:PermissionKey"];
        }
        public async Task<T> GetPermissionsData<T>()
        {
            string stringifiedData = await _cache.GetStringAsync(PERMISSIONSKEY);
            return JsonConvert.DeserializeObject<T>(stringifiedData);
        }

        public async Task SetPermissionsData(object data)
        {
            string stringifiedData = JsonConvert.SerializeObject(data, Formatting.Indented);
            await _cache.SetStringAsync(PERMISSIONSKEY, stringifiedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddYears(100),
                SlidingExpiration = new TimeSpan(10000, 0, 0, 0, 0)
            });
        }

        public async Task<T> GetReportData<T>(string cachekey)
        {
            string stringifiedData = await _cache.GetStringAsync(cachekey);
            return JsonConvert.DeserializeObject<T>(stringifiedData);
        }

        


        public async Task SetReportData(object data, string cachekey)
        {
            string stringifiedData = JsonConvert.SerializeObject(data, Formatting.Indented);
            await _cache.SetStringAsync(cachekey, stringifiedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                SlidingExpiration = new TimeSpan(10000, 0, 0, 0, 0)
            });
        }

        public async Task RemoveReportCache(string cachekey)
        {
            await _cache.RemoveAsync(cachekey);
        }

        public async Task SetSignalRToken(string data, string cachekey)
        {            
            await _cache.SetStringAsync(cachekey, data, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                SlidingExpiration = new TimeSpan(1, 0, 0, 0, 0)
            });
        }
        public async Task<string> GetSignalRToken(string cachekey)
        {
            return await _cache.GetStringAsync(cachekey);
        }
    }
}
