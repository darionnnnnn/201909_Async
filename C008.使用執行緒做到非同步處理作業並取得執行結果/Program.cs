using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C008.使用執行緒做到非同步處理作業並取得執行結果
{
    class Program
    {
        static object lockObj = new object();

        static void Main(string[] args)
        {
            // 不使用 lock 可發現 int 沒有 Thread-safety
            int shareData = 0;

            Console.WriteLine($"建立新執行緒物件");
            Thread thread1 = new Thread(() =>
            {
                Console.WriteLine($"執行緒 1 的 ID={Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 2000000; i++)
                {
                    lock (lockObj)
                    {
                        shareData += 1;
                    }
                }
                Console.WriteLine();
            });

            Thread thread2 = new Thread(() =>
            {
                Console.WriteLine($"執行緒 2 的 ID={Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 2000000; i++)
                {
                    lock (lockObj)
                    {
                        shareData += 1;
                    }
                }
                Console.WriteLine();
            });

            Console.WriteLine($"啟動執行緒");

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine($"執行緒 執行完畢");
            Console.WriteLine($"執行緒 執行結果為 {shareData}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
