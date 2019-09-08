using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeminiApi.Models.Requests
{
    public class OrderStatusRequest : BasicRequest
    {
        [JsonProperty("order_id")]
        public string OrderId { get; internal set; }

        public OrderStatusRequest(string url, string orderId) : base(url)
        {
            OrderId = orderId;
        }
    }
}
