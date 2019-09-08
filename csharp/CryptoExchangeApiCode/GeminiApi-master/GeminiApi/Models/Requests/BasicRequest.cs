using System;
using Newtonsoft.Json;

namespace GeminiApi.Models.Requests
{
    public class BasicRequest
    {
        [JsonProperty("request")]
        public string Request { get; internal set; }

        [JsonProperty("nonce")]
        public long Nonce { get; internal set; }

        public BasicRequest(string request)
        {
            Request = request;
            Nonce = DateTimeToUnixTimestamp(DateTime.Now);
        }

        private long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            return unixTimeStampInTicks / TimeSpan.TicksPerMillisecond;
        }
    }
}
