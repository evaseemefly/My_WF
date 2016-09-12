using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF2
{

    public sealed class BaoxiaoApprove : CodeActivity
    {
        public InArgument<int> Money { get; set; } 
        public InArgument<string> Reason { get; set; } 

        public OutArgument<bool> Approve { get; set; } 
        protected override void Execute(CodeActivityContext context)
        {
            string reason = context.GetValue(Reason);
            int money = context.GetValue(Money);
            Console.WriteLine("报销事由：{0}，报销金额：{1}",reason,money);

            Console.WriteLine("请输入审批意见：(y/n)");
            string a1 = Console.ReadLine();

            if (a1.ToLower().Equals("y"))
            {
                context.SetValue(Approve, true);
            }
            else
            {
                context.SetValue(Approve,false);
            }
        }
    }
}
