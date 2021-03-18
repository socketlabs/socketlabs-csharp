using System;

namespace SocketLabs.InjectionApi
{
    /// <summary>
    /// 
    /// </summary>
    public class RetrySettings
    {

        /// <summary>
        /// The maximum number of retries when sending an Injection API Request before throwing an exception. Default: 0, no retries, you must explicitly enable retry settings
        /// </summary>
        public int MaximumNumberOfRetries { get; }

        /// <summary>
        /// The minimum wait time between between HTTP retries. Default: 1s
        /// </summary>
        public TimeSpan MinimumRetryTimeBetween { get; }
        
        /// <summary>
        /// The maximum wait time between between retries. Default: 10s
        /// </summary>
        public TimeSpan MaximumRetryTimeBetween { get; }

        private const int _defaultNumberOfRetries = 0;
        private const int _maximumAllowedNumberOfRetries = 5;
        private readonly TimeSpan _defaultMinimumRetryTime = TimeSpan.FromSeconds(1);
        private readonly TimeSpan _defaultMaximumRetryTime = TimeSpan.FromSeconds(10);

        /// <summary>
        ///  Creates a new instance of the <c>RetrySettings</c>.
        /// </summary>
        /// <param name="maximumNumberOfRetries"></param>
        /// <param name="minimumRetryTimeBetween"></param>
        /// <param name="maximumRetryTimeBetween"></param>
        public RetrySettings(int? maximumNumberOfRetries = null, TimeSpan? minimumRetryTimeBetween = null, TimeSpan? maximumRetryTimeBetween = null)
        {
            
            if (maximumNumberOfRetries != null)
            {
                if (maximumNumberOfRetries < 0) throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "maximumNumberOfRetries must be greater than 0");
                if (maximumNumberOfRetries > 5) throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), $"The maximum number of allowed retries is {_maximumAllowedNumberOfRetries}");
                
                MaximumNumberOfRetries = maximumNumberOfRetries.Value;
            }
            else
                MaximumNumberOfRetries = _defaultNumberOfRetries;



            if (minimumRetryTimeBetween != null)
            {
                if (minimumRetryTimeBetween.Value.Ticks < 0) throw new ArgumentOutOfRangeException(nameof(minimumRetryTimeBetween), "minimumRetryTimeBetween must be greater than 0");

                MinimumRetryTimeBetween = minimumRetryTimeBetween.Value;
            }
            else
                MinimumRetryTimeBetween = _defaultMinimumRetryTime;


            if (maximumRetryTimeBetween != null)
            {
                if (maximumRetryTimeBetween.Value.Ticks < 0) throw new ArgumentOutOfRangeException(nameof(maximumRetryTimeBetween), "maximumRetryTimeBetween must be greater than 0");
                if (maximumRetryTimeBetween.Value.TotalSeconds > 30) throw new ArgumentOutOfRangeException(nameof(maximumRetryTimeBetween), "maximumRetryTimeBetween must be less than 30 seconds");

                MaximumRetryTimeBetween = maximumRetryTimeBetween.Value;
            }
            else
                MaximumRetryTimeBetween = _defaultMaximumRetryTime;


            if (minimumRetryTimeBetween != null && maximumRetryTimeBetween != null)
            {
                if (minimumRetryTimeBetween.Value.TotalMilliseconds > maximumRetryTimeBetween.Value.TotalMilliseconds)
                    throw new ArgumentOutOfRangeException(nameof(minimumRetryTimeBetween),
                        "minimumRetryTimeBetween must be less than maximumRetryTimeBetween");
            }


        }
    }
}
