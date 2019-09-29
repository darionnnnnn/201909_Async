using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D030.非同步與例外異常
{
    class 工作尚未啟動就發生例外異常
    {
        static void Main(string[] args)
        {
            //var fooTask = Task.Run(() =>
            var fooTask = Task.Factory.StartNew(() =>
            {
                // 當在工作內發生了例外異常，應用程式不會受到影響
                Thread.Sleep(1000);
                throw new InvalidProgramException("發生了例外異常");
            });

            fooTask.ContinueWith(x =>
            {
                Console.WriteLine($"Status : {x.Status}");
                Console.WriteLine($"IsCompleted : {x.IsCompleted}");
                Console.WriteLine($"IsCanceled : {x.IsCanceled}");
                Console.WriteLine($"IsFaulted : {x.IsFaulted}");
            });

            while (fooTask.Status != TaskStatus.WaitingForActivation)
            {
                Console.WriteLine($"Status : {fooTask.Status}");
                Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
                Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
                Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
                break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
