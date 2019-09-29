using System.Web;
using System.Web.Mvc;

namespace D010.ASP_NET專案對於同步內容的使用
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
