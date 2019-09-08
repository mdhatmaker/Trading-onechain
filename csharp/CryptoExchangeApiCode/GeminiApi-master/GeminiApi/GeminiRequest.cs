using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GeminiApi.Models;
using GeminiApi.Models.Requests;
using GeminiApi.Models.Responses;
using Newtonsoft.Json;

namespace GeminiApi
{
    public class GeminiRequest
    {
        private static string _baseUrl = "";
        private static string _key = "";
        private static string _secret = "";

        public GeminiRequest(string key, string secret, string baseUrl = "https://api.sandbox.gemini.com")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is absent");
            }

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new Exception("Secret is absent");
            }

            _baseUrl = baseUrl;
            _key = key;
            _secret = secret;
        }

        public BasicResponse GetHearbeat()
        {
            return Send<BasicResponse>(new HeartBeat());
        }

        public List<BalancesResponse> GetAvailableBalances()
        {
            return Send<List<BalancesResponse>>(new GetAvailableBalances());
        }

        public OrderResponse CreateNewOrder(decimal price, decimal amount, string side = Side.Buy, string symbol = Symbol.BtcToUsd)
        {
            return Send<OrderResponse>(new NewOrder()
            {
                Side = side,
                Price = price,
                Amount = amount,
                Symbol = symbol
            });
        }

        public List<OrderResponse> GetActiveOrders()
        {
            return Send<List<OrderResponse>>(new GetActiveOrders());
        }

        public OrderResponse GetOrderStatus(string orderId)
        {
            return Send<OrderResponse>(new GetOrderStatus(orderId));
        }

        public BasicResponse CancelOrder(string orderId)
        {
            return Send<BasicResponse>(new CancelOrder(orderId));
        }

        public BasicResponse CancelAllOrder()
        {
            return Send<BasicResponse>(new CancelAllOrder());
        }

        private T Send<T>(BasicRequest request)
        {
            try
            {
                var jsonText = JsonConvert.SerializeObject(request);
                var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonText));

                var hmac = new HMACSHA384(Encoding.UTF8.GetBytes(_secret));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var hexHash = BitConverter.ToString(hash).Replace("-", "");

                using (var client = new WebClient())
                {
                    client.Headers.Add("X-GEMINI-APIKEY", _key);
                    client.Headers.Add("X-GEMINI-PAYLOAD", payload);
                    client.Headers.Add("X-GEMINI-SIGNATURE", hexHash);

                    var response = client.UploadString($"{_baseUrl}{request.Request}", "");

                    return JsonConvert.DeserializeObject<T>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
