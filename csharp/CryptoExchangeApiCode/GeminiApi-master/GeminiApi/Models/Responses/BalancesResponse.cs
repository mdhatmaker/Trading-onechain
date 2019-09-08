using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeminiApi.Models.Responses
{
    public class BalancesResponse
    {
        [JsonProperty("currency")]
        public string Currency { get; internal set; }

        [JsonProperty("amount")]
        public decimal Amount { get; internal set; }

        [JsonProperty("available")]
        public decimal Available { get; internal set; }

        [JsonProperty("availableForWithdrawal")]
        public decimal AvailableForWithdrawal { get; internal set; }
    }
}
