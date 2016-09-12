using System.Web;
using System.Web.Mvc;
using HM13OA.UI.Models;

namespace HM13OA.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyErrorHandleAttribute());
        }
    }
}