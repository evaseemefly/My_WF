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
using System.Activities.DurableInstancing;
using System.Configuration;

namespace WWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AutoResetEvent syncEven = new AutoResetEvent(false);
        //WorkflowApplication application = null;
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("当前线程："+Thread.CurrentThread.ManagedThreadId.ToString());
            //WorkflowApplication application = new WorkflowApplication(new Activity1(), new Dictionary<string, object>()
            //{
            //    {"InputDateTime",DateTime.Now.ToString() },
            //    {"TempBookMark",this.txtBookMarkName.Text }
            //});

            //需要传入参数
            WorkflowApplication application = new WorkflowApplication(new Activity1(), new Dictionary<string, object>()
            {
                {"InputDateTime",DateTime.Now.ToString() },
                {"TempBookMark",this.txtBookMarkName.Text }
            });

            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(ConfigurationManager.AppSettings["workflowConnection"]);

            application.InstanceStore = store;

            application.Unloaded += OnUnloaded;
            application.Aborted += OnAborted;
            application.Idle += OnIdle;
            application.PersistableIdle += OnPersistableIdle;
            application.OnUnhandledException += OnUnhandledException;
            this.txtWFID.Text = application.Id.ToString();
            //application.Load(application.Id);
            //application.ResumeBookmark(this.txtBookMarkName.Text, int.Parse(this.txtValue.Text));
            application.Run();
            //Console.WriteLine("主线程即将被阻止，等待唤醒");
            //syncEven.WaitOne();
            //Console.WriteLine("主线程继续执行");
        }

        private UnhandledExceptionAction OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("主线程：异常处理");
            syncEven.Set();
            return UnhandledExceptionAction.Abort;
        }

        private PersistableIdleAction OnPersistableIdle(WorkflowApplicationIdleEventArgs arg)
        {
            Console.WriteLine("主线程：工作流持久化");
            return PersistableIdleAction.Unload;
        }

        private void OnIdle(WorkflowApplicationIdleEventArgs obj)
        {
            syncEven.Set();
            Console.WriteLine("主线程：工作流空闲");
        }

        private void OnAborted(WorkflowApplicationAbortedEventArgs obj)
        {
            syncEven.Set();
            Console.WriteLine("主线程：工作流中止");
        }

        private void OnUnloaded(WorkflowApplicationEventArgs obj)
        {
            syncEven.Set();
            Console.WriteLine("主线程：工作流卸载");
        }

        /// <summary>
        /// 唤醒bookMark执行流程，并将输入的数字传入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //application.ResumeBookmark(this.txtBookMarkName.Text, int.Parse(this.txtValue.Text));
            WorkflowApplication application = new WorkflowApplication(new Activity1());

            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(ConfigurationManager.AppSettings["workflowConnection"]);

            application.InstanceStore = store;

            application.Unloaded += OnUnloaded;
            application.Aborted += OnAborted;
            application.Idle += OnIdle;
            application.PersistableIdle += OnPersistableIdle;
            application.OnUnhandledException += OnUnhandledException;
            //this.txtWFID.Text = application.Id.ToString();
            application.Load(Guid.Parse(this.txtWFID.Text));
            application.ResumeBookmark(this.txtBookMarkName.Text, int.Parse(this.txtValue.Text));



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
