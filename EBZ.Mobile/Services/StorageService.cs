using Akavache;
using EBZ.Mobile.ServicesInterface;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class StorageService : IStorageService
    {
        private IBlobCache Cache;

        public StorageService(IBlobCache cache = null)
        {
            BlobCache.ApplicationName = "eaglesbyteEBZ";
            Cache = cache ?? BlobCache.Secure;           
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await Cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public async Task<IDictionary<string, T>> GetKeyPairsFromCache<T>(IEnumerable<string> keys)
        {
            try
            {
                IDictionary<string, T> t = await Cache.GetObjects<T>(keys);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(IDictionary<string, T>);
            }
        }

        public async void InsertIntoCache<T>(string cacheName, T t)
        {
            await Cache.InsertObject<T>(cacheName, t);
        }

        public async void InsertIntoObjectCache<T>(string cacheName, T t)
        {
            await Cache.InsertObject<T>(cacheName, t);
        }

        public async void InsertKeyPairsIntoCache<T>(Dictionary<string, T> dictionary)
        {
            await Cache.InsertObjects<T>(dictionary);
        }

        public void InvalidateCaches<T>()
        {
            Cache.InvalidateAllObjects<T>();
        }

        public void InvalidateCache<T>(string key)
        {
            Cache.InvalidateObject<T>(key);
        }

        public void InvalidateCache(string key)
        {
            Cache.Invalidate(key);
        }

        public void InvalidateAllCache()
        {
            Cache.InvalidateAll();
        }
    }
}
