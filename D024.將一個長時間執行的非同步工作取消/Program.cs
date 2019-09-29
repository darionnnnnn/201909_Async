using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D024.將一個長時間執行的非同步工作取消
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

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

            try
            {
                await MyMethodAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{Environment.NewLine}下載已經取消");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Environment.NewLine}發現例外異常 {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static async Task MyMethodAsync(CancellationToken token)
        {
            await Task.Run(() =>
            {
                int cc = 0;
                while(true)
                {
                    if (token.IsCancellationRequested == true)
                        break;
                    if (cc++ % 10 == 0) Console.Write(".");
                    Thread.Sleep(100);
                }
            });

        }
    }
}
