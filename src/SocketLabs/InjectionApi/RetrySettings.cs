using System;

namespace SocketLabs.InjectionApi
{
    /// <summary>
    /// 
    /// </summary>
    public class RetrySettings
    {

        private const int _defaultNumberOfRetries = 0;
        private const int _maximumAllowedNumberOfRetries = 5;
        private readonly TimeSpan _minimumRetryTime = TimeSpan.FromSeconds(1);
        private readonly TimeSpan _maximumRetryTime = TimeSpan.FromSeconds(10);

        /// <summary>
        ///  Creates a new instance of the <c>RetrySettings</c>.
        /// </summary>
        /// <param name="maximumNumberOfRetries"></param>
        public RetrySettings(int? maximumNumberOfRetries = null)
        {
            
            if (maximumNumberOfRetries != null)
            {
                if (maximumNumberOfRetries < 0) throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "maximumNumberOfRetries must be greater than 0");
                if (maximumNumberOfRetries > 5) throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), $"The maximum number of allowed retries is {_maximumAllowedNumberOfRetries}");
                
                MaximumNumberOfRetries = maximumNumberOfRetries.Value;
            }
            else
                MaximumNumberOfRetries = _defaultNumberOfRetries;
            
        }


        /// <summary>
        /// The maximum number of retries when sending an Injection API Request before throwing an exception. Default: 0, no retries, you must explicitly enable retry settings
        /// </summary>
        public int MaximumNumberOfRetries { get; }
        

        /// <summary>
        /// Get the time period to wait before next call
        /// </summary>
        /// <param name="numberOfAttempts"></param>
        /// <returns></returns>
        public TimeSpan GetNextWaitInterval(int numberOfAttempts)
        {
            var interval = (int)Math.Min(
                _minimumRetryTime.TotalMilliseconds + GetRetryDelta(numberOfAttempts),
                _maximumRetryTime.TotalMilliseconds);

            return TimeSpan.FromMilliseconds(interval);
        }
        internal virtual int GetRetryDelta(int numberOfAttempts)
        {
            var random = new Random();

            var min = (int)(TimeSpan.FromSeconds(1).TotalMilliseconds * 0.8);
            var max = (int)(TimeSpan.FromSeconds(1).TotalMilliseconds * 1.2);

            return (int)((Math.Pow(2.0, numberOfAttempts) - 1.0) * random.Next(min, max));
        }
    }
}
