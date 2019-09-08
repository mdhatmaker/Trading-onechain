using System;
using System.Collections.Generic;
using GeminiApi.Models.Requests;

namespace GeminiApi.Models
{
    public class HeartBeat : BasicRequest
    {
        public HeartBeat() : base("/v1/heartbeat") { }
    }

    public class NewOrder : OrderRequest
    {
        public NewOrder() : base("/v1/order/new") { }
    }

    public class CancelAllOrder : BasicRequest
    {
        public CancelAllOrder() : base("/v1/order/cancel/all") { }
    }

    public class CancelOrder : OrderStatusRequest
    {
        public CancelOrder(string orderId) : base("/v1/order/cancel", orderId) { }
    }

    public class GetAvailableBalances : BasicRequest
    {
        public GetAvailableBalances() : base("/v1/balances") { }
    }

    public class GetOrderStatus : OrderStatusRequest
    {
        public GetOrderStatus(string orderId) : base("/v1/order/status", orderId) { }
    }

    public class GetActiveOrders : BasicRequest
    {
        public GetActiveOrders() : base("/v1/orders") { }
    }
}
