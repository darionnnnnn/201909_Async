using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C004.APM.AsyncCallback委派結束非同步作業
{
    // 使用 AsyncCallback 委派結束非同步作業
    //
    // 下列程式碼範例會示範使用 HttpWebRequest 類別來非同步方法存取網路服務。 
    // 此範例會建立參考 ResponseCallback 方法的 AsyncCallback 委派。
    // 這個方法會對 DNS 資訊的每個非同步要求都呼叫一次。
    class Program
    {
        private static void ResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest request = ar.AsyncState as HttpWebRequest;
            Console.WriteLine($"取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
            HttpWebResponse webResponse = request.EndGetResponse(ar) as HttpWebResponse;//取得資料
            Console.WriteLine($"成功取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");

            Stream ReceiveStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(ReceiveStream);
            string result = reader.ReadToEnd();

            Console.WriteLine($"網頁執行結果:{result}");

        }
        static void Main(string[] args)
        {
            try
            {
                // 這個網址服務將會提供將兩個整數相加起來，不過，可以指定要暫停幾秒鐘
                string host = "https://lobworkshop.azurewebsites.net";
                string path = "/api/RemoteSource/Add/15/43/5";
                string url = $"{host}{path}";

                // 針對非同步請求，產生委派方法，用於處理非同步工作執行完成後的結果
                AsyncCallback callBack = new AsyncCallback(ResponseCallback);

                // 使用 WebRequest.Create 工廠方法建立一個 HttpWebrequest 物件
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(url);

                // 呼叫 BeginXXX 啟動非同步工作
                Console.WriteLine($"啟動 APM 非同步方法 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                IAsyncResult asyncResult =
                  (IAsyncResult)myHttpWebRequest1.BeginGetResponse(callBack, myHttpWebRequest1);

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"   處理其他事情 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                    Thread.Sleep(1000);
                }

                // 主執行緒的工作已經完成
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (WebException e)
            {
                Console.WriteLine("\nException raised!");
                Console.WriteLine("\nMessage:{0}", e.Message);
                Console.WriteLine("\nStatus:{0}", e.Status);
                Console.WriteLine("Press any key to continue..........");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException raised!");
                Console.WriteLine("Source :{0} ", e.Source);
                Console.WriteLine("Message :{0} ", e.Message);
                Console.WriteLine("Press any key to continue..........");
                Console.Read();
            }
        }
    }
}
