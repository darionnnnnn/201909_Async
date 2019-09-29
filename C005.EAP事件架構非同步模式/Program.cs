﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C005.EAP事件架構非同步模式
{
    // 這是使用 EAP Event-based Asynchronous Pattern 範例
    // 這個樣式特色為啟動非同步呼叫之後，就會再另外一個 Thread 繼續來執行這些非同步的需求，不會封鎖呼叫執行緒
    // 當完成之後，會透過 Call Back來繼續處理相關工作，而是否完成與完成的結果內容，可以透過這個委派事件參數取得
    // 
    // 將非同步功能公開到用戶端程式碼的方式有許多種； 事件架構非同步模式會針對要呈現非同步行為之類別指示一個方法。
    // https://msdn.microsoft.com/zh-tw/library/ms228969(v=vs.110).aspx
    class Program
    {
        // 請觀察除錯點上的 Thread ID
        public static void Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Add/15/43/5";
            string url = $"{host}{path}";

            var wc = new WebClient();
            // 指定當 在非同步資源下載作業完成時發生 ，要執行的委派方法
            wc.DownloadStringCompleted += (s, e) =>
            {
                Console.WriteLine($"已經完成非同步方法執行的 回呼 callback 的執行緒 ID {Thread.CurrentThread.ManagedThreadId}");

                // 完成非同步呼叫之後，可以透過委派事件內的參數，取得此次非同步工作的失敗/成功狀態，完成結果內容

                // 判斷此次非同步呼叫結果，依據該事件回傳的參數來決定
                if (e.Error != null)
                    Console.WriteLine(e.Error);
                else if (e.Cancelled)
                    Console.WriteLine("取消");
                else
                {
                    Console.WriteLine($"成功取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                    Console.WriteLine(e.Result);
                }
            };

            Console.WriteLine($"啟動 EAP 非同步方法 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
            // 下載指定的資源做為 System.Uri。這個方法不會封鎖呼叫執行緒
            wc.DownloadStringAsync(new Uri(url));

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"   處理其他事情 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
