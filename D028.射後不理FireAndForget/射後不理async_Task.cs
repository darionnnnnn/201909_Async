using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D028.射後不理FireAndForget
{
    class 射後不理async_Task
    {
        static void Main(string[] args)
        {
            Console.WriteLine("對於一個非同步工作方法，採用射後不理，將會正常結束 ...");
            OnDelegateAsync();

            Console.WriteLine("Press any key for 了解當非同步工作內拋出例外異常，會如何？");
            Console.ReadKey();

            // 為何這行不會造成應用程式崩潰
            var fooTask = OnDelegateWithExceptionAsync();

            Console.WriteLine("Press any key for 查看例外異常內容(不過，等到非同步工作結束才能看的到)...");
            Console.ReadKey();

            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatusX}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static async Task OnDelegateAsync()
        {
            Console.WriteLine("OnDelegateAsync 方法開始執行了");
            await Task.Delay(5000);
            Console.WriteLine("OnDelegateAsync 方法結束執行了");
        }
        static async Task OnDelegateWithExceptionAsync()
        {
            Console.WriteLine("OnDelegateWithExceptionAsync 開始執行了");
            await Task.Delay(5000);
            throw new Exception("有例外異常發生了");
            Console.WriteLine("OnDelegateWithExceptionAsync 方法結束執行了");
        }
    }
}
