using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Aop.Framework;

namespace t2_AOPTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //ITestClass t1 = new TestClass();
            //t1.aopTest();

            ProxyFactory factory = new ProxyFactory(new TestClass());
            factory.AddAdvice(new BeforeAdvice());   
            factory.AddAdvice(new AfterAdvice());
            factory.AddAdvice(new AroundAdvice());
            ITestClass helloProxy = (ITestClass)factory.GetProxy();

            helloProxy.aopTest();


            Console.ReadKey();

        }
    }
}
