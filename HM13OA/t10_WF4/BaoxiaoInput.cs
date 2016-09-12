using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF4
{

    public sealed class BaoxiaoInput : CodeActivity
    {
        public InArgument<string>  Reason { get; set; }
        public InArgument<int> Money { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string reason = context.GetValue(Reason);
            int money = context.GetValue(Money);

            Console.WriteLine("事由：{0}，金额：{1}",reason,money);
        }
    }
}
