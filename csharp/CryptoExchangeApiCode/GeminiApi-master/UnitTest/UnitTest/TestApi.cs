using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeminiApi;

namespace UnitTest
{
    [TestClass]
    public class TestApi
    {
        private static string _key = "pVMzjlL8TUDX2fe8OODw";
        private static string _secret = "29fxP948vJM3FfknBArV3fmaTY8E";

        private GeminiRequest _gr = new GeminiRequest(_key, _secret);

        [TestMethod]
        public void CheckHearbeat()
        {
            var result = _gr.GetHearbeat();
            Assert.IsTrue(result.Result == "ok");
        }

        [TestMethod]
        public void GetAvailableBalances()
        {
            var result = _gr.GetAvailableBalances();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetOrderStatus()
        {
            var resultNewOrder =  _gr.CreateNewOrder(10, 100);
            var resultGetStatus = _gr.GetOrderStatus(resultNewOrder.OrderId.ToString());
            if (resultNewOrder.Timestamp != resultGetStatus.Timestamp)
            {
                Assert.Fail();
            }

            _gr.CancelAllOrder();
        }

        [TestMethod]
        public void CancelOrder()
        {
            var resultNewOrder = _gr.CreateNewOrder(10, 10);

            _gr.CancelOrder(resultNewOrder.OrderId);

            var resultGetStatus = _gr.GetOrderStatus(resultNewOrder.OrderId.ToString());
            if (!resultGetStatus.IsCancelled)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetActiveOrders()
        {
            _gr.CancelAllOrder();
            _gr.CreateNewOrder(10, 100);
            var orders = _gr.GetActiveOrders();

            if (orders.Count != 1)
            {
                Assert.Fail();
            }

             _gr.CreateNewOrder(10, 100);
            orders = _gr.GetActiveOrders();

            if (orders.Count != 2)
            {
                Assert.Fail();
            }

            _gr.CancelAllOrder();
        }
    }
}
