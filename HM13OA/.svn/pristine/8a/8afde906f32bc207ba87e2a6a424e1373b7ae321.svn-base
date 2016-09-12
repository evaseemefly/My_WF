using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF3
{

    public sealed class BaoxiaoInput : CodeActivity
    {
        //public OutArgument<string>  Reason { get; set; }
        //public OutArgument<int>  Money { get; set; }

        public InArgument<string> Reason { get; set; } 
        public InArgument<int> Money { get; set; } 

        protected override void Execute(CodeActivityContext context)
        {
            //Console.WriteLine("请输入事由：");
            //string reason = Console.ReadLine();
            //Console.WriteLine("请输入金额：");
            //int money = int.Parse(Console.ReadLine());

            //context.SetValue(Reason,reason);
            //context.SetValue(Money,money);

            string reason = context.GetValue(Reason);
            int money = context.GetValue(Money);

            Console.WriteLine("事由：{0}，金额：{1}",reason,money);
        }
    }
}
