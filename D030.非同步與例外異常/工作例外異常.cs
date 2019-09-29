using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D030.非同步與例外異常
{
    class 工作例外異常
    {
        static void Main(string[] args)
        {
            var fooTask = Task.Run(() =>
            {
                throw new InvalidProgramException("發生了例外異常");
            });

            Thread.Sleep(800);
            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatusX}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
