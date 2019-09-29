using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace D001.GUI應用程式的造成死結範例
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        StringBuilder sb = new StringBuilder();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnRunBlockForWait_Click(object sender, RoutedEventArgs e)
        {
            sb.Clear();
            sb.Append($"*** 使用 ConfigureAwait(false) 來避免死結 ***{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}");

            var fooTask = GetRemoteResult2Async();
            var foo = fooTask.Result;

            sb.Append($"呼叫非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫 Web API 的執行結果 {foo}");
            tbkResult.Text = sb.ToString();
        }

        private async void btnRunAllAsync_Click(object sender, RoutedEventArgs e)
        {
            sb.Clear();
            sb.Append($"*** 全程都使用 await 非同步方法呼叫 來避免死結 ***{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}");

            var foo = await GetRemoteResult1Async();

            sb.Append($"呼叫非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫 Web API 的執行結果 {foo}");
            tbkResult.Text = sb.ToString();
        }

        private async void btnHasDeadlock_Click(object sender, RoutedEventArgs e)
        {
            sb.Clear();
            sb.Append($"*** 會造成死結 ***{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}");

            var fooClientTask = GetRemoteResult1Async();
            var foo = fooClientTask.Result;

            sb.Append($"呼叫非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
            sb.Append($"呼叫 Web API 的執行結果 {foo}");
            tbkResult.Text = sb.ToString();
        }

        public async Task<string> GetRemoteResult1Async()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                sb.Append($"呼叫讀取 Web API 非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
                var result = await client.GetStringAsync(
                    "https://lobworkshop.azurewebsites.net/api/RemoteSource/Add/88/11/3");
                sb.Append($"呼叫讀取 Web API 非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
                return result;
            }
        }

        public async Task<string> GetRemoteResult2Async()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                sb.Append($"呼叫讀取 Web API 非同方法 前 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
                var result = await client.GetStringAsync(
                    "https://lobworkshop.azurewebsites.net/api/RemoteSource/Add/88/11/3")
                    .ConfigureAwait(false);
                sb.Append($"呼叫讀取 Web API 非同方法 後 的執行緒 : {Thread.CurrentThread.ManagedThreadId}{Environment.NewLine}{Environment.NewLine}");
                return result;
            }
        }
    }
}
