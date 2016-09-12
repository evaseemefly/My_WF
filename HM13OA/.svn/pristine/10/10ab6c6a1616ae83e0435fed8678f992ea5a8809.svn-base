using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace HM13OA.Workflow
{

    public sealed class BaoxiaoInput : NativeActivity
    {
        public InArgument<string> Reason2 { get; set; } 
        public InArgument<int> Money2 { get; set; }
        public InArgument<bool> Approve2 { get; set; } 

        public OutArgument<string> Reason { get; set; }
        public OutArgument<int> Money { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            bool a2 = context.GetValue(Approve2);
            if (!a2)
            {
                //被驳回后执行此段代码
                context.CreateBookmark("Input1", FuncInput);
            }
            else
            {
                //第一次发起，执行此段代码
                string reason2 = context.GetValue(Reason2);
                int money2 = context.GetValue(Money2);

                context.SetValue(Reason,reason2);
                context.SetValue(Money,money2);
            }
        }

        private void FuncInput(NativeActivityContext context, Bookmark bookmark, object value)
        {
            BaoxiaoModel model = value as BaoxiaoModel;
            context.SetValue(Reason,model.Reason);
            context.SetValue(Money,model.Money);
        }
    }
}
