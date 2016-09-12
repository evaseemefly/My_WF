using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WWF
{

    public sealed class Wait4BookMark : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> Text { get; set; }
        public InArgument<string> BookMarkName { get; set; }

        public OutArgument<int> Num { get; set; }

        /// <summary>
        /// 创建bookMark，让流程停下来
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(NativeActivityContext context)
        {
            string bookMarkName = context.GetValue(BookMarkName);
            context.CreateBookmark(bookMarkName, new BookmarkCallback(ConExecuteWorkFlow));
        }

        /// <summary>
        /// 回调函数
        /// 为输出参数Num赋值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bookMark"></param>
        /// <param name="value"></param>
        public void ConExecuteWorkFlow(NativeActivityContext context,Bookmark bookMark,object value)
        {
            context.SetValue(Num, (int)value);
        }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

    }
}
