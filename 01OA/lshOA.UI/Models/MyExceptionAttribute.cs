using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Models
{
    /// <summary>
    /// 自定义的异常处理特性需要继承自异常特性
    /// </summary>
    public class MyExceptionAttribute:HandleErrorAttribute
    {
        //对于可能出现的多个用户出现异常时，为了避免并发的问题，将异常信息都写入一个队列中，队列中只调用唯一的线程向文件写入

        /// <summary>
        /// 错误队列
        /// </summary>
        public static Queue<Exception> exceptionQueue = new Queue<Exception>();

        /// <summary>
        /// 发生异常时调用的方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //1 将异常处理上下文中的异常对象加入到异常队列中
            exceptionQueue.Enqueue(filterContext.Exception);
            //2 跳转至错误页面
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}