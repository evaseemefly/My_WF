using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Spring.Web.Mvc;
using System.Threading;
using log4net;
using lshOA.UI.Models;

namespace lshOA.UI
{
    public class MvcApplication : SpringMvcApplication /*System.Web.HttpApplication*/
    {
        //相当于窗体程序的main函数
        protected void Application_Start()
        {
            IndexManager.GetInstance().CreateThread();
            log4net.Config.XmlConfigurator.Configure(); //读取log4net配置信息

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string fileLogPath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while(true)
                {
                    //1 判断错误队列中是否有数据
                    if (Models.MyExceptionAttribute.exceptionQueue.Count() > 0)
                    {
                        //1.1 出列
                        Exception ex = Models.MyExceptionAttribute.exceptionQueue.Dequeue();

                        if (ex != null)
                        {
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                }
                
            },fileLogPath);
        }
    }
}
