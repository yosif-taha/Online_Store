using Online_Store.Domain.Contracts;
using Online_Store.Services.Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Cache
{
    public class CachService(ICacheRepository _repository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
           var result = await _repository.GetAsync(key);
            return result;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
          await  _repository.SetAsync(key, value, duration);
        }
    }
}
