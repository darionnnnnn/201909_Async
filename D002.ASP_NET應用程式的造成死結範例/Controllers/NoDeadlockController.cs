using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace D002.ASP_NET應用程式的造成死結範例.Controllers
{
    public class NoDeadlockController : ApiController
    {
        StringBuilder sb = new StringBuilder();
        public async Task<string> Get()
        {
            sb.Clear();
            sb.Append($"*** 全程都使用 await 非同步方法呼叫 來避免死結 ***{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}");

            var foo = await GetRemoteResult1Async();

            sb.Append($"呼叫非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫 Web API 的執行結果 {foo}");
            return sb.ToString();
        }

        private async Task<string> GetRemoteResult1Async()
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
