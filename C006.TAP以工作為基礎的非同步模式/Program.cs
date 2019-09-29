using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C006.TAP以工作為基礎的非同步模式
{
    /// <summary>
    /// 這個專案說明了如何使用 TAP Task-based Asynchronous Pattern 來呼叫非同步的工作
    /// 在這個設計模式下，我們不需要呼叫 BeginXXX/EndXXX，也不再需要透過事件 Call Back來得知是否工作完成，
    /// 取而代之的是，我們不再需要使用任何複雜的非同步程式設計方式來寫程式碼，而是使用同步的方式來寫程式，
    /// 但寫出來的程式是具有非同步效果，並且不會造成 Thread Block
    /// 
    /// https://msdn.microsoft.com/zh-tw/library/hh873175(v=vs.110).aspx
    /// </summary>
    class Program
    {
        // 請觀察除錯點上的 Thread ID
        static async Task Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Add/15/43/5";
            string url = $"{host}{path}";

            // 注意下列並沒有依照底下說明進行設定，將無法使用 await，這是因為在App的 Entry Point內，無法使用 Async關鍵字
            // 請滑鼠雙擊專案的 [Property] 節點，切換到標籤頁次 [建置] > [進階]，在 [進階建置設定]對話窗內
            // 選擇 [一般] > [語言版本]，選擇 C# 7.1 以上版本

            HttpClient httpClient = new HttpClient();

            Console.WriteLine($"啟動 TAP 非同步方法 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
            var task = httpClient.GetStringAsync(url);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"   處理其他事情 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
            string result = task.Result;
            Console.WriteLine($"成功取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
            Console.WriteLine(result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
