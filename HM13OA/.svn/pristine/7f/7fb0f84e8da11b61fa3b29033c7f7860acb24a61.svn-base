using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopAlliance.Intercept;

namespace t2_AOPTest
{
    public interface ITestClass
    {
        void aopTest();
    }


    public class TestClass : ITestClass
    {
        public TestClass()
        {
            System.Console.WriteLine("TestClass 被创建");
        }
        public void aopTest()
        {
            Console.WriteLine("TestClass 方法执行 ");
        }
    }


    public class BeforeAdvice : Spring.Aop.IMethodBeforeAdvice
    {
        public void Before(System.Reflection.MethodInfo method, object[] args, object target)
        {
            Console.WriteLine("前置通知");
        }
    }

    public class AfterAdvice : Spring.Aop.IAfterReturningAdvice
    {

        public void AfterReturning(object returnValue, System.Reflection.MethodInfo method, object[] args, object target)
        {
            Console.WriteLine("后置通知");

        }
    }

    public class AroundAdvice : IMethodInterceptor
    {

        public object Invoke(IMethodInvocation invocation)
        {
            Console.WriteLine("环绕通知");
            invocation.Proceed();
            return "";
        }
    }
}
