using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace HM13OA.Workflow
{

    public sealed class BaoxiaoApprove : NativeActivity
    {
        public InArgument<string>  Reason { get; set; }
        public InArgument<int> Money { get; set; }

        public OutArgument<bool> Approve { get; set; } 

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            string reason = context.GetValue(Reason);
            int money = context.GetValue(Money);
            Console.WriteLine("事由：{0}，金额：{1}",reason,money);

            context.CreateBookmark("Approve", FuncApprove);
        }

        private void FuncApprove(NativeActivityContext context, Bookmark bookmark, object value)
        {
            bool approve = Convert.ToBoolean(value);
            context.SetValue(Approve,approve);
        }
    }
}
