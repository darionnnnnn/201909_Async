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

            var tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", (i + 1));

                tasks[i] = Task.Run( async () =>
                {
                    HttpClient client = new HttpClient();
                    // 取得 Thread ID > Thread.CurrentThread.ManagedThreadId
                    // 偵錯時常用到
                    var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);
;
                    var task2 = client.GetStringAsync(url);


                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    //var result = client.GetStringAsync(url).Result;
                    var result = await client.GetStringAsync(url);
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {result}");
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    result = await client.GetStringAsync(url);
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {result}");
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
                });

            }


            var task = Task.WhenAll(tasks);

            while (!task.IsCompleted)
            {
                Console.WriteLine("*");
                // 記得要等 await
                await Task.Delay(1000);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

// 保哥版
//using System;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;

//namespace D008.利用TAP工作建立大量並行工作練習
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            string host = "https://lobworkshop.azurewebsites.net";
//            string path = "/api/RemoteSource/Source3";
//            string url = $"{host}{path}";

//            Stopwatch sw = new Stopwatch();

//            sw.Start();

//            var tasks = new Task[10];

//            for (int i = 0; i < 10; i++)
//            {
//                var index = string.Format("{0:D2}", (i + 1));

//                tasks[i] = Task.Run(async () =>
//                {
//                    HttpClient client = new HttpClient();

//                    var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

//                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
//                    var result = await client.GetStringAsync(url);
//                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {result}");
//                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

//                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");
//                    result = await client.GetStringAsync(url);
//                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {result}");
//                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
//                });
//            }

//            var task = Task.WhenAll(tasks);

//            while (!task.IsCompleted)
//            {
//                Console.WriteLine("*");
//                await Task.Delay(1000);
//            }

//            sw.Stop();
//            Console.WriteLine($"總執行時間 {sw.ElapsedMilliseconds} 毫秒!");

//            Console.WriteLine("Press any key to continue...");
//            Console.ReadKey();
//        }
//    }
//}