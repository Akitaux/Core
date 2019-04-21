using System;
using System.Net;
using Voltaic;

namespace Akitaux
{
    public class RestException : Exception
    {
        public HttpStatusCode HttpCode { get; }
        public Utf8String Reason { get; }

        public RestException(HttpStatusCode httpCode, Utf8String reason = null)
            : base(CreateMessage(httpCode, reason))
        {
            HttpCode = httpCode;
            Reason = reason;
        }

        private static string CreateMessage(HttpStatusCode httpCode, Utf8String reason = null)
        {
            if (!(reason is null) && reason.Bytes.Length > 0)
                return $"The server responded with error {(int)httpCode}: {reason.ToString()}";
            else
                return $"The server responded with error {(int)httpCode}: {httpCode.ToString()}";
        }
    }
}
