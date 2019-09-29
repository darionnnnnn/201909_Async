using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D030.非同步與例外異常
{
    class 同步程式碼例外異常
    {
        static void Main(string[] args)
        {
            Console.WriteLine("程式開始執行");
            // 當在執行緒內發生了例外異常，應用程式將會結束執行
            throw new InvalidProgramException($"發生了例外異常");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
