using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D009.主執行緒與背景執行緒特性
{
    class Program
    {
        // Thread 使用的方法
        public static void Main()
        {
            int maxLoop = 300;
            new Thread(() =>
            {
                for (int i = 0; i < maxLoop; i++) { Console.Write("B"); }
            })
            { IsBackground = true }.Start();

            new Thread(() =>
            {
                for (int i = 0; i < maxLoop / 50; i++) { Console.Write("F"); }
            })
            { IsBackground = false }.Start();

            ThreadPool.QueueUserWorkItem((x) =>
            {
                for (int i = 0; i < maxLoop; i++) { Console.Write("P"); }
            });

            for (int i = 0; i < maxLoop / 50; i++) { Console.Write("M"); }

            //Console.WriteLine("請按任一鍵，以結束執行");
            //Console.ReadKey();
        }
    }
}
