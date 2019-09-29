using System.Web;
using System.Web.Mvc;

namespace D002.ASP_NET應用程式的造成死結範例
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
