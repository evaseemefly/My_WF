using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.DurableInstancing;
using System.ServiceModel.Activities.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HM13OA.Common;

namespace t10_WF4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            #region 手动调用
            //Activity baoxiao=new BaoxiaoStateMachine();
            //WorkflowApplication app = new WorkflowApplication(
            //    baoxiao,
            //    new Dictionary<string, object>()
            //    {
            //        {"R2",txtReason.Text},
            //        {"M2",int.Parse(txtMoney.Text)}
            //    }
            //    );

            ////设置空闲卸载
            //app.PersistableIdle = a => { return PersistableIdleAction.Unload; };

            ////配置卸载时，持久化到哪里去
            //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["WFStore"].ConnectionString;
            //InstanceStore store=new SqlWorkflowInstanceStore(conn);
            //app.InstanceStore = store;

            //txtGuid.Text = app.Id.ToString();


            //app.Run(); 
            #endregion

            #region 调用帮助类

            txtGuid.Text= WorkflowHelper.Run(
                new BaoxiaoStateMachine(),
                new Dictionary<string, object>()
                {
                    {"R2", txtReason.Text},
                    {"M2", int.Parse(txtMoney.Text)}
                }
                ).ToString();

            #endregion
        }

        private void btnJLApprove_Click(object sender, EventArgs e)
        {
            #region 手动调用
            //Activity baoxiao=new BaoxiaoStateMachine();
            //WorkflowApplication app=new WorkflowApplication(baoxiao);

            //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["WFStore"].ConnectionString;
            //InstanceStore store=new SqlWorkflowInstanceStore(conn);
            //app.InstanceStore = store;

            //app.Load(Guid.Parse(txtGuid.Text));

            //bool approve = radioButton1.Checked ? true : false;
            //app.ResumeBookmark("JLApprove", approve); 
            #endregion

            #region 调用帮助类
            WorkflowHelper.Resume(
                new BaoxiaoStateMachine(), 
                Guid.Parse(txtGuid.Text),
                "JLApprove",
                radioButton1.Checked
                );
            #endregion
        }
    }
}
