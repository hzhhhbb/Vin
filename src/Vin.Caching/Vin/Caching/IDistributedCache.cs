using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Vin.Caching
{
    public interface IDistributedCache<in TCacheKey, TCacheItem> where TCacheItem : class
    {
        /// <summary>
        ///     Gets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <returns>The located value or null.</returns>
        TCacheItem Get(TCacheKey key);

        /// <summary>
        ///     Gets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the located value or null.</returns>
        Task<TCacheItem> GetAsync(TCacheKey key, CancellationToken token = default);

        /// <summary>
        ///     Sets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        /// <param name="options">The cache options for the value.</param>
        void Set(TCacheKey key, TCacheItem value, DistributedCacheEntryOptions options);

        /// <summary>
        ///     Sets a value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="value">The value to set in the cache.</param>
        void Set(TCacheKey key, TCacheItem value);

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
        Task SetAsync(TCacheKey key, TCacheItem value, DistributedCacheEntryOptions options, CancellationToken token = default);

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
        Task SetAsync(TCacheKey key, TCacheItem value,CancellationToken token = default);

        /// <summary>
        ///     Refreshes a value in the cache based on its key, resetting its sliding expiration timeout (if any).
        /// </summary>
        /// <param name="key">A string identifying the requested calue.</param>
        void Refresh(TCacheKey key);

        /// <summary>
        ///     Refreshes a value in the cache based on its key, resetting its sliding expiration timeout (if any).
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        Task RefreshAsync(TCacheKey key, CancellationToken token = default);

        /// <summary>
        ///     Removes the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        void Remove(TCacheKey key);

        /// <summary>
        ///     Removes the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">
        ///     Optional. The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        Task RemoveAsync(TCacheKey key, CancellationToken token = default);
    }

    public interface IDistributedCache<TCacheItem> : IDistributedCache<string, TCacheItem> where TCacheItem : class
    {
    }
}