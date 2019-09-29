using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D003.Console應用程式的不會造成死結範例
{
    class Program
    {
        static StringBuilder sb = new StringBuilder();
        static void Main(string[] args)
        {
            sb.Clear();
            sb.Append($"*** Console 不會造成死結 ***{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}");

            var fooClientTask = GetRemoteResult1Async();
            var foo = fooClientTask.Result;

            sb.Append($"呼叫非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫 Web API 的執行結果 {foo}");

            Console.WriteLine(sb.ToString());

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static async Task<string> GetRemoteResult1Async()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                sb.Append($"呼叫讀取 Web API 非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
                var result = await client.GetStringAsync(
                    "https://lobworkshop.azurewebsites.net/api/RemoteSource/Add/88/11/3");
                sb.Append($"呼叫讀取 Web API 非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
                return result;
            }
        }
    }
}
