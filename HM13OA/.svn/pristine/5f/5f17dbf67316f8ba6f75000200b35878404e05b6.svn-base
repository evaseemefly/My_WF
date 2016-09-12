using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF3
{

    public sealed class BaoxiaoApprove : NativeActivity//CodeActivity
    {
        public InArgument<string> Reason { get; set; } 
        public InArgument<int> Money { get; set; }
        public OutArgument<bool> Approve { get; set; } 

        //protected override void Execute(CodeActivityContext context)
        //{
        //    string reason = context.GetValue(Reason);
        //    int money = context.GetValue(Money);
        //    //Console.WriteLine("事由：{0}，金额：{1}",reason,money);

        //    Console.WriteLine("请输入审批意见：(y/n)");
        //    string approve = Console.ReadLine();
        //    if (approve.ToLower().Equals("y"))
        //    {
        //        context.SetValue(Approve, true);
        //    }
        //    else
        //    {
        //        context.SetValue(Approve,false);
        //    }
        //}

        protected override void Execute(NativeActivityContext context)
        {
            Console.WriteLine("遇到书签，进入空闲");
            Bookmark bookmark = context.CreateBookmark("Approve1", Func1);
        }

        private void Func1(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Console.WriteLine("从回调点继续运行");
            Console.WriteLine("审批意见：{0}",value);
            context.SetValue(Approve,Convert.ToBoolean(value));
        }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }
    }
}
