using System.Web;
using System.Web.Mvc;

namespace lshOA.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //添加指定过滤器
            filters.Add(new Models.MyExceptionAttribute());
        }
    }
}
