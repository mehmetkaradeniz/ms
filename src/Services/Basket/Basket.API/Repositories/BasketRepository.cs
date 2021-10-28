using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<BasketEntity> Get(string username)
        {
            var basketJson = await _redisCache.GetStringAsync(username);
            if (String.IsNullOrEmpty(basketJson))
                return null;

            return JsonConvert.DeserializeObject<BasketEntity>(basketJson);
        }

        public async Task<BasketEntity> Update(BasketEntity basket)
        {
            if (Get(basket.Username) == null)
                return null;

            await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));

            return await Get(basket.Username);
        }

        public async Task Delete(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

    }
}
