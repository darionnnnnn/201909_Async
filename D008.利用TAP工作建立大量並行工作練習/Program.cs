using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        static string url = "https://lobworkshop.azurewebsites.net/api/RemoteSource/Source3";

        static async Task Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();

            sw.Start();

            var tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", (i + 1));

                tasks[i] = Task.Run(async () =>
                {
                    Task task1 = GetPageAsync(index, 1);
                    Task task2 = GetPageAsync(index, 2);

                    try
                    {
                        await Task.WhenAll(task1, task2);
                    }
                    catch (Exception ex)
                    {
                        throw;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                });
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.BackgroundColor = ConsoleColor.Black;
            }

            sw.Stop();
            Console.WriteLine($"總執行時間 {sw.ElapsedMilliseconds} 毫秒!");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static async Task<string> GetPageAsync(string index, int num)
        {
            HttpClient client = new HttpClient();

            var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"{index}-{num} 測試 (TID: {tid}) >>>> {DateTime.Now}");

            if (index.IndexOf("5") >= 0)
            {
                throw new ArgumentException("5 is invalid");
            }

            var result = await client.GetStringAsync(url);
            Console.WriteLine($"{index}-{num} 測試 (TID: {tid}) ==== {result}");
            Console.WriteLine($"{index}-{num} 測試 (TID: {tid}) <<<< {DateTime.Now}");

            return result;
        }
    }
}