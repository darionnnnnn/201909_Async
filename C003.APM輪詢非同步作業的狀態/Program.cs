using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C003.APM輪詢非同步作業的狀態
{
    // 輪詢非同步作業的狀態，以便得知該非同步作業是否已經完成了
    // 
    // 下列程式碼範例會示範使用 HttpWebRequest 類別來非同步方法存取網路服務。  
    // 這個範例會啟動非同步作業，然後會在作業完成之前在主控台列印句號 (".")。 
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 這個網址服務將會提供將兩個整數相加起來，不過，可以指定要暫停幾秒鐘
                string host = "https://lobworkshop.azurewebsites.net";
                string path = "/api/RemoteSource/Add/15/43/5";
                string url = $"{host}{path}";

                // 使用 WebRequest.Create 工廠方法建立一個 HttpWebrequest 物件
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(url);

                // 呼叫 BeginXXX 啟動非同步工作
                Console.WriteLine($"啟動 APM 非同步方法 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                IAsyncResult asyncResult =
                  (IAsyncResult)myHttpWebRequest1.BeginGetResponse(null, null);

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"   處理其他事情 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                    Thread.Sleep(1000);
                }

                // 開始進行輪詢非同步作業的狀態，看看是否已經完成
                // 我們會使用 . 句號輸出，表示這個應用程式還在執行中，並沒有封鎖起來

                // 在這個回圈內，會不斷地查看非同步工作是否已經完成，透過 IsCompleted 成員
                // 這樣做法雖然不會造成 Thread Block，可以，可以看得出來，這樣做法會耗用大量的 CPU 資源
                // 因為，我們需要不斷的察看非同步工作是否已經完成
                while (asyncResult.IsCompleted != true)
                {
                    // 我們會使用 . 句號輸出，表示這個應用程式還在執行中，並沒有封鎖起來
                    Console.WriteLine($"   . (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                }

                // 若程式已經可以執行到這裡，那就表示非同步工作已經完成了
                Console.WriteLine($"取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                WebResponse webResponse = myHttpWebRequest1.EndGetResponse(asyncResult);
                Console.WriteLine($"成功取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");

                Stream ReceiveStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(ReceiveStream);
                string result = reader.ReadToEnd();

                Console.WriteLine($"網頁執行結果:{result}");

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
