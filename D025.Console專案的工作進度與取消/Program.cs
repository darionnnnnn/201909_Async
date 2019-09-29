using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D025.Console專案的工作進度與取消
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "http://http.speed.hinet.net/test_010m.zip";
            CancellationTokenSource cts = new CancellationTokenSource();

            var progress = new Progress<int>((s) =>
            {
                Console.WriteLine($"下載進度為 {s}%");
            });

            #region 等候使用者輸入 取消 c 按鍵
            ThreadPool.QueueUserWorkItem(x =>
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.C)
                {
                    cts.Cancel();
                }
            });
            #endregion

            await 下載檔案內容(url, cts.Token, progress);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static async Task 下載檔案內容(string page, CancellationToken token, IProgress<int> progress)
        {
            #region 使用 HttpClient 與 TAP 模式來設計
            await Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page, HttpCompletionOption.ResponseHeadersRead, token))
                    {
                        long? contentLength = response.Content.Headers.ContentLength;
                        using (HttpContent content = response.Content)
                        {
                            using (var httpStream = await content.ReadAsStreamAsync())
                            {
                                long 串流總共長度 = contentLength.Value;
                                long 串流已讀取長度 = 0;
                                byte[] buffer = new byte[1024];
                                while (true)
                                {
                                    int read = await httpStream.ReadAsync(buffer, 0, buffer.Length, token);
                                    if (read <= 0)
                                        break;
                                    串流已讀取長度 += read;
                                    if (progress != null)
                                    {
                                        progress.Report((int)(串流已讀取長度 * 100 / 串流總共長度));
                                    }
                                }
                            }
                        }
                    }
                }
            });
            #endregion
            return;
        }
    }
}
