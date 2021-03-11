using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Vin.Caching
{
    public class DistributedCache<TCacheItem> : DistributedCache<string, TCacheItem>,IDistributedCache<TCacheItem>,IDistributedCache<string,TCacheItem> where TCacheItem : class
    {
        public DistributedCache(IDistributedCacheSerializer serializer, IDistributedCache cache) : base(serializer, cache)
        {
        }
    }

    public class DistributedCache<TCacheKey, TCacheItem> : IDistributedCache<TCacheKey, TCacheItem> where TCacheItem : class
    {
        public DistributedCache(IDistributedCacheSerializer serializer, IDistributedCache cache)
        {
            Serializer = serializer;
            Cache = cache;
        }

        protected IDistributedCache Cache { get; }
        protected IDistributedCacheSerializer Serializer { get; }

        /// <summary>
        ///     Gets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <returns>The located value or null.</returns>
        public TCacheItem Get(TCacheKey key)
        {
            var cachedItem = Cache.Get(NormalizeKey(key));
            return cachedItem!=null?Serializer.Deserialize<TCacheItem>(cachedItem):default;
        }

        /// <summary>
        ///     Gets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the located value or null.</returns>
        public async Task<TCacheItem> GetAsync(TCacheKey key, CancellationToken token = default)
        {
            var cachedBytes = await Cache.GetAsync(NormalizeKey(key), token);
            return cachedBytes != null ? Serializer.Deserialize<TCacheItem>(cachedBytes) : default(TCacheItem);
        }

        /// <summary>
        ///     Sets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        /// <param name="options">The cache options for the value.</param>
        public void Set(TCacheKey key, TCacheItem value, DistributedCacheEntryOptions options)
        {
            Cache.Set(NormalizeKey(key), Serializer.Serialize(value), options);
        }

        /// <summary>
        ///     Sets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        public void Set(TCacheKey key, TCacheItem value)
        {
            Set(key, value, new DistributedCacheEntryOptions());
        }

        /// <summary>
        ///     Sets the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        /// <param name="options">The cache options for the value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        public async Task SetAsync(TCacheKey key, TCacheItem value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            var serializedBytes = Serializer.Serialize(value);
            await Cache.SetAsync(NormalizeKey(key), serializedBytes, options, token);
        }

        /// <summary>
        ///     Sets the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        public async Task SetAsync(TCacheKey key, TCacheItem value, CancellationToken token = default)
        {
            await SetAsync(key, value, new DistributedCacheEntryOptions(), token);
        }

        /// <summary>
        ///     Refreshes a value in the cache based on its key, resetting its sliding expiration timeout (if any).
        /// </summary>
        /// <param name="key">A string identifying the requested calue.</param>
        public void Refresh(TCacheKey key)
        {
            Cache.Refresh(NormalizeKey(key));
        }

        /// <summary>
        ///     Refreshes a value in the cache based on its key, resetting its sliding expiration timeout (if any).
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        public async Task RefreshAsync(TCacheKey key, CancellationToken token = default)
        {
            await Cache.RefreshAsync(NormalizeKey(key), token);
        }

        /// <summary>
        ///     Removes the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        public void Remove(TCacheKey key)
        {
            Cache.Remove(NormalizeKey(key));
        }

        /// <summary>
        ///     Removes the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        public async Task RemoveAsync(TCacheKey key, CancellationToken token = default)
        {
            await Cache.RemoveAsync(NormalizeKey(key), token);
        }

        private string NormalizeKey(TCacheKey key)
        {
            return key.ToString();
        }
    }
}