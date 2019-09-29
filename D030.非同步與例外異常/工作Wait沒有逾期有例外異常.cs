using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D030.非同步與例外異常
{
    class 工作Wait沒有逾期有例外異常
    {
        static void Main(string[] args)
        {
            var fooTask = Task.Run(async () =>
            {
                await Task.Delay(1500);
                throw new InvalidProgramException("發生了例外異常");
            });

            var result = fooTask.Wait(3000);

            Console.WriteLine($"此次執行的逾期狀態為 {!result}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
