using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF1
{

    public sealed class Calc : CodeActivity
    {
        public InArgument<int> Num1 { get; set; } 
        public InArgument<int> Num2 { get; set; } 

        protected override void Execute(CodeActivityContext context)
        {
            //通过上下文从输入参数中获取值
            int num1 = context.GetValue(Num1);
            int num2 = context.GetValue(Num2);

            int result = num1 + num2;
            Console.WriteLine(result);
        }
    }
}
