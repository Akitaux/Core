// https://github.com/discord-net/Wumpus.Net/blob/master/src/Wumpus.Net.Rest/Net/Throttling/RateLimitInfo.cs

using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Akitaux
{
    public class RateLimitInfo
    {
        public const string DefaultGlobalHeader = "X-RateLimit-Global";
        public const string DefaultLimitHeader = "X-RateLimit-Limit";
        public const string DefaultRemainingHeader = "X-RateLimit-Remaining";
        public const string DefaultResetHeader = "X-RateLimit-Reset";
        public const string DefaultRetryHeader = "Retry-After";
        public const string DefaultDateHeader = "Date";

        public bool IsGlobal { get; }
        public int? Limit { get; }
        public int? Remaining { get; }
        public int? RetryAfter { get; }
        public DateTimeOffset? Reset { get; }
        public TimeSpan? Lag { get; }

        internal RateLimitInfo(HttpResponseHeaders headers)
        {
            IsGlobal = headers.TryGetValues(DefaultGlobalHeader, out var values) &&
                bool.TryParse(values.First(), out var isGlobal) ? isGlobal : false;
            Limit = headers.TryGetValues(DefaultLimitHeader, out values) &&
                int.TryParse(values.First(), out var limit) ? limit : (int?)null;
            Remaining = headers.TryGetValues(DefaultRemainingHeader, out values) &&
                int.TryParse(values.First(), out var remaining) ? remaining : (int?)null;
            Reset = headers.TryGetValues(DefaultResetHeader, out values) &&
                int.TryParse(values.First(), out var reset) ? DateTimeOffset.FromUnixTimeSeconds(reset) : (DateTimeOffset?)null;
            RetryAfter = headers.TryGetValues(DefaultRetryHeader, out values) &&
                int.TryParse(values.First(), out var retryAfter) ? retryAfter : (int?)null;
            Lag = headers.TryGetValues(DefaultDateHeader, out values) &&
                DateTimeOffset.TryParse(values.First(), out var date) ? DateTimeOffset.UtcNow - date : (TimeSpan?)null;
        }
    }
}
