extern  alias  MyLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HM13OA.Common;
using myLog=MyLog::log4net;

namespace HM13OA.UI.Models
{
    public class MyErrorHandleAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //异常处理记录
            //LogHelper.WriteLog(filterContext.Exception.StackTrace);
            myLog.ILog log = myLog.LogManager.GetLogger("WebLogger");
            log.Debug(filterContext.Exception.StackTrace);

            //转到错误提示页
            filterContext.Result=new RedirectResult("/Error.html");
        }
    }
}