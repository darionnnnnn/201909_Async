using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace D020.將AMP改成TAP
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 這個網址服務將會提供將兩個整數相加起來，不過，可以指定要暫停幾秒鐘
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Add/22/77/1";
            string url = $"{host}{path}";

            WebRequest myHttpWebRequest1 = WebRequest.Create(url);

            WebResponse webResponse = await Task.Factory.FromAsync(myHttpWebRequest1.BeginGetResponse, myHttpWebRequest1.EndGetResponse, null);

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                string result = await reader.ReadToEndAsync();
                Console.WriteLine($"網頁執行結果:{result}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
