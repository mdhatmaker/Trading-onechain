using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GeminiApi.Models.Requests
{
    public class OrderRequest : BasicRequest
    {
        [JsonProperty("symbol")]
        public string Symbol { get; internal set; }

        [JsonProperty("amount")]
        public decimal Amount { get; internal set; }

        [JsonProperty("price")]
        public decimal Price { get; internal set; }

        [JsonProperty("side")]
        public string Side { get; internal set; }

        [JsonProperty("type")]
        public string Type { get { return "exchange limit"; } }

        public OrderRequest(string url) : base(url) { }
    }
}
