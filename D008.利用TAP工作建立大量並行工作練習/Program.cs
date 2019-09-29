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
        static void Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";

            HttpClient client = new HttpClient();

            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", (i + 1));

                Task.Run(() =>
                {
                    var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    var result = client.GetStringAsync(url).Result;
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {result}");
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    result = client.GetStringAsync(url).Result;
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {result}");
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
                });
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
