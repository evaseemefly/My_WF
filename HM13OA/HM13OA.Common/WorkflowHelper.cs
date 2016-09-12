using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.Common
{
    public class WorkflowHelper
    {
        public static Guid Run(Activity activity,IDictionary<string,object> dic)
        {
            WorkflowApplication app=new WorkflowApplication(activity,dic);

            //空闲卸载
            app.PersistableIdle = a =>
            {
                return PersistableIdleAction.Unload;
            };

            //持久化存储
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["WFStore"].ConnectionString;
            app.InstanceStore = new SqlWorkflowInstanceStore(conn); ;

            //启动工作流
            app.Run();

            return app.Id;
        }

        public static void Resume(Activity activity,Guid guid,string bookmarkName,object value)
        {
            WorkflowApplication app=new WorkflowApplication(activity);

            //空闲卸载
            app.PersistableIdle = a =>
            {
                return PersistableIdleAction.Unload;
            };

            //持久化存储
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["WFStore"].ConnectionString;
            app.InstanceStore = new SqlWorkflowInstanceStore(conn); ;

            //加载
            app.Load(guid);

            //继续
            app.ResumeBookmark(bookmarkName, value);

        }
    }
}
