using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace t10_WF3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Activity baoxiao=new BaoxiaoFlowChart();

            WorkflowInvoker.Invoke(baoxiao);
        }

        private WorkflowApplication app;
        private void btnStart_Click(object sender, EventArgs e)
        {
            string reason = txtReason.Text;
            int money = int.Parse(txtMoney.Text);

            Activity baoxiao=new BaoxiaoFlowChart();
             app=new WorkflowApplication(
                baoxiao,
                new Dictionary<string, object>()
                {
                    {"R2",reason},
                    {"M2",money}
                });

            app.Run();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            bool approve = radioButton1.Checked ? true : false;

            app.ResumeBookmark("Approve1", approve);
        }
    }
}
