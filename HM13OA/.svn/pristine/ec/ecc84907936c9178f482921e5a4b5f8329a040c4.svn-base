using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF2
{

    public sealed class BaoxiaoInput : CodeActivity
    {
        public OutArgument<int> Money { get; set; } 
        public OutArgument<string> Reason { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Console.WriteLine("请输入报销事由：");
            string reason = Console.ReadLine();

            Console.WriteLine("请输入报销金额：");
            int money = int.Parse(Console.ReadLine());

            context.SetValue(Reason,reason);
            context.SetValue(Money,money);
        }
    }
}
