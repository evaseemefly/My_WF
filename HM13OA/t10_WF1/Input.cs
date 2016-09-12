using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace t10_WF1
{

    public sealed class Input : CodeActivity
    {
        // 表示可以从别的活动中，传递过来一个字符串，使用这个参数来接收，即这是一个输入参数
        //public InArgument<string> Text { get; set; }
        //// 表示可以向活动外，输出一个字符串，直接赋值给T1，即输出参数
        //public OutArgument<string> T1 { get; set; } 

        public OutArgument<int> Num { get; set; } 


        /// <summary>
        /// 当工作流执行到这个代码活动时，会调用Execute方法执行，如果希望这个代码活动实现某个功能，直接在Execute方法中写代码即可
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            string p1=Console.ReadLine();
            context.SetValue(Num,int.Parse(p1));//通过上下文向输出参数中设置值
        }
    }
}
