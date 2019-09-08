using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoWebSocketApis
{
    public class BinanceWebSocket
    {
        public static void TestBinanceTickers()
        {
            const int BYTE_SIZE = 1024;

            Console.WriteLine("LAUNCH: Binance");

            var socket = new ClientWebSocket();
            Task task = socket.ConnectAsync(new Uri("wss://stream.binance.com:9443/stream?streams=btcusdt@trade/ethusdt@trade/ethbtc@trade"), CancellationToken.None);
            task.Wait();

            Thread readThread = new Thread(
                delegate (object obj)
                {
                    byte[] recBytes = new byte[BYTE_SIZE];
                    while (true)
                    {
                        ArraySegment<byte> t = new ArraySegment<byte>(recBytes);
                        Task<WebSocketReceiveResult> receiveAsync = socket.ReceiveAsync(t, CancellationToken.None);
                        receiveAsync.Wait();
                        string jsonString = Encoding.UTF8.GetString(recBytes);
                        //Console.WriteLine("jsonString = {0}", jsonString);

                        JObject jo = JsonConvert.DeserializeObject<JObject>(jsonString);
                        string stream = jo["stream"].Value<string>();
                        jo = jo["data"].Value<JObject>();
                        string type = jo["e"].Value<string>();
                        //Console.WriteLine("stream: {0}   eventType: {1}", stream, type);


                        if (type == "trade")                // "trade": 
                        {
                            //string pid = jo["product_id"].Value<string>();
                            int i = stream.IndexOf("@");
                            string pid = stream.Substring(0, i);
                            string price = jo["p"].Value<string>();
                            long time = jo["E"].Value<long>();
                            string size = jo["q"].Value<string>();
                            Console.WriteLine("{0}  {1} {2}  {3}", pid, price, size, time);
                        }
                        /*else if (type == "update")          // "update"
                        {
                            //Console.WriteLine(jsonString);
                        }
                        else if (type == "error")           // "error"
                        {
                        }
                        else if (type == "heartbeat")       // "heartbeat"
                        {
                        }*/

                        Array.Clear(recBytes, 0, BYTE_SIZE);
                        //recBytes = new byte[BYTE_SIZE];
                    }
                });
            readThread.Start();

            //Console.ReadLine();
        }

    } // end of class Binance
} // end of namespace
