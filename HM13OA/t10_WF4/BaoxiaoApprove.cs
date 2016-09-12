using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF4
{

    public sealed class BaoxiaoApprove : NativeActivity
    {
        public OutArgument<bool>  Approve { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark("JLApprove", Func1);
        }

        private void Func1(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(Approve,Convert.ToBoolean(value));
        }

    }
}
