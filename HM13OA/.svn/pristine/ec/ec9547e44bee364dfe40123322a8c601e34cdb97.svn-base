using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;
using Spring.Core;

namespace t1_IoCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //1、创建容器对象
            IApplicationContext ctx = ContextRegistry.GetContext();

            //2、声明接口类型的变量
            IPerson p;

            //3、通过容器创建对象
            //p = ((IObjectFactory) ctx).GetObject("Zxh") as IPerson;
            p = ctx.GetObject<IPerson>("Zxh");

            //4、完成对象成员的调用
            p.Say();

            Console.ReadKey();
        }
    }
}
