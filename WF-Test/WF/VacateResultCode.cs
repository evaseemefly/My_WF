using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WF
{

    public sealed class VacateResultCode : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        //public InArgument<string> Text { get; set; }
        public InArgument<int> Days { get; set; }
        public OutArgument<int> Result { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            int day = context.GetValue(Days);

            if(day<2)
            {
                context.SetValue(Result, 0);
            }
            else if(day<5)
            {
                context.SetValue(Result, 1);
            }
            else
            {
                context.SetValue(Result, 2);
            }
        }
    }
}
