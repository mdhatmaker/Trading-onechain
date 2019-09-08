using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeminiApi.Models.Responses
{
    public class BasicResponse
    {
        [JsonProperty("result")]
        public string Result { get; internal set; }
    }
}
