using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D011.工作啟動與狀態
{
    class 委派類型的工作
    {
        static void Main(string[] args)
        {
            string lastStatus = "";
            Task monitorTask;
            bool IsBegin = false;

            monitorTask = new Task(() =>
            {
                Thread.Sleep(3000);
                //throw new Exception("喔喔，發生例外異常");
            });

            #region 監視工作狀態是否已經有變更，並且顯示出最新的狀態值
            new Thread(() =>
            {
                while (true)
                {
                    var tmpStatus = monitorTask.Status;
                    if (tmpStatus.ToString() != lastStatus)
                    {
                        Console.WriteLine($"Status : {monitorTask.Status}");
                        Console.WriteLine($"IsCompleted : {monitorTask.IsCompleted}");
                        Console.WriteLine($"IsCanceled : {monitorTask.IsCanceled}");
                        Console.WriteLine($"IsFaulted : {monitorTask.IsFaulted}");
                        var exceptionStatusX = (monitorTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
                        Console.WriteLine($"Exception : {exceptionStatusX}");
                        Console.WriteLine();
                        lastStatus = tmpStatus.ToString();
                    }
                }
            })
            { IsBackground = false }.Start();

            #endregion

            Thread.Sleep(300);
            monitorTask.Start();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
