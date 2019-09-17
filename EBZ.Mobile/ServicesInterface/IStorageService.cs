using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IStorageService
    {
        Task<T> GetFromCache<T>(string cacheName);
        void InsertIntoCache<T>(string cacheName, T t);
        void InsertKeyPairsIntoCache<T>(Dictionary<string, T> dictionary);
        Task<IDictionary<string, T>> GetKeyPairsFromCache<T>(IEnumerable<string> keys);
        void InvalidateCaches<T>();
        void InvalidateCache<T>(string key);
        void InvalidateCache(string key);
        void InvalidateAllCache();
    }
}
