using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeminiApi.Models.Responses
{
    public class OrderResponse
    {
        [JsonProperty("order_id")]
        public string OrderId { get; internal set; }

        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; internal set; }

        [JsonProperty("symbol")]
        public string Symbol { get; internal set; }

        [JsonProperty("exchange")]
        public string Exchange { get; internal set; }

        [JsonProperty("price")]
        public decimal Price { get; internal set; }

        [JsonProperty("avg_execution_price")]
        public decimal AvgExecutionPrice { get; internal set; }

        [JsonProperty("side")]
        public string Side { get; internal set; }

        [JsonProperty("type")]
        public string Type { get; internal set; }

        [JsonProperty("options")]
        public List<string> Options { get; internal set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; internal set; }

        [JsonProperty("timestampms")]
        public string TimestampMs { get; internal set; }

        [JsonProperty("is_live")]
        public bool IsLive { get; internal set; }

        [JsonProperty("is_cancelled")]
        public bool IsCancelled { get; internal set; }

        [JsonProperty("was_forced")]
        public bool WasForced { get; internal set; }

        [JsonProperty("executed_amount")]
        public decimal ExecutedAmount { get; internal set; }

        [JsonProperty("remaining_amount")]
        public decimal RemainingAmount { get; internal set; }

        [JsonProperty("original_amount")]
        public decimal OriginalAmount { get; internal set; }
    }
}
