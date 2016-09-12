using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WF
{

    public sealed class InputVacateCode : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> Text { get; set; }
        public OutArgument<int> Days { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            int num;
            string n = Console.ReadLine();
            int.TryParse(n, out num);
            context.SetValue(Days, num);
            // 获取 Text 输入参数的运行时值
            //string text = context.GetValue(this.Text);
        }
    }
}
