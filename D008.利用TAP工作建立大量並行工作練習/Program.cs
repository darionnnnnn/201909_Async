using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        //static void Main(string[] args)
        static async Task Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";

            // 多執行緒共用一個 HttpClient
            // HttpClient is Thread-safety 但同時只能發出一個 request，等於多個執行緒互等
            //HttpClient client = new HttpClient();

            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", (i + 1));

                Task.Run( async () =>
                {
                    HttpClient client = new HttpClient();
                    // 取得 Thread ID > Thread.CurrentThread.ManagedThreadId
                    // 偵錯時常用到
                    var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

                    var task1 = client.GetStringAsync(url);
                    var task2 = client.GetStringAsync(url);

                    await Task.WhenAll(task1, task2);

                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    //var result = client.GetStringAsync(url).Result;
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {task1.Result}");
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {task2.Result}");
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
                });

            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
