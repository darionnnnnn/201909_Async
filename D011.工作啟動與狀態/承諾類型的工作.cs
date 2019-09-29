using System;
using System.Threading;
using System.Threading.Tasks;

namespace D011.工作啟動與狀態
{
    class 承諾類型的工作
    {
        static void Main(string[] args)
        {
            string lastStatus = "";
            Task monitorTask = new Task(() => Thread.Sleep(0));

            // 是要為 false 就會離開！
            bool IsBegin = true;

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

                    if (IsBegin == false) return;
                }
            })
            {
                // 前景執行緒不結束，主執行緒就不會結束！
                IsBackground = false
            }.Start(); // 不會真的立刻啟動

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            monitorTask = tcs.Task;

            Thread.Sleep(1000);

            var key = Console.ReadKey();
            if (key.KeyChar == 'e')
            {
                tcs.SetResult(null);
                Thread.Sleep(500);
                IsBegin = false;
            }

            //tcs.SetCanceled();
            //tcs.SetException(new Exception("喔喔，發生例外異常"));

            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();

            #endregion
        }
    }
}
