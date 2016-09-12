using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AutoResetEvent syncEven = new AutoResetEvent(false);
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("当前线程："+Thread.CurrentThread.ManagedThreadId.ToString());
            WorkflowApplication application = new WorkflowApplication(new Activity1(), new Dictionary<string, object>()
            {
                {"InputDateTime",DateTime.Now.ToString() }
            });

            //application.Unloaded += OnUnloaded;
            //application.Aborted += OnAborted;
            //application.Idle += OnIdle;
            //application.PersistableIdle += OnPersistableIdle;
            //application.OnUnhandledException += OnUnhandledException;
            application.Run();
            syncEven.WaitOne();
            Console.WriteLine("主线程继续执行");
        }

        private UnhandledExceptionAction OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("异常处理");
            syncEven.Set();
            return UnhandledExceptionAction.Abort;
        }

        private PersistableIdleAction OnPersistableIdle(WorkflowApplicationIdleEventArgs arg)
        {
            Console.WriteLine("工作流持久化");
            return PersistableIdleAction.Unload;
        }

        private void OnIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("工作流空闲");
        }

        private void OnAborted(WorkflowApplicationAbortedEventArgs obj)
        {
            syncEven.Set();
            Console.WriteLine("工作流中止");
        }

        private void OnUnloaded(WorkflowApplicationEventArgs obj)
        {
            syncEven.Set();
            Console.WriteLine("工作流卸载");
        }
    }
}
