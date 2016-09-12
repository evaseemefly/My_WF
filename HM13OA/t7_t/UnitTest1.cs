using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace t7_t
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //测试用例
            int a = 1, b = 2;

            //调用测试方法
            Calc c1=new Calc();
            int result1 = c1.Add(a, b);

            //手动实现测试方法的功能
            int result2 = a + b;

            //设置断言
            Assert.AreEqual(result1, result2);
        }
    }

    public partial class Calc
    {
        public int Add(int a, int b)
        {
            return a + b+1;
        }
    }
}
