using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace t6_ExpressionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用lambda来构造Expression对象
            //Expression<Func<Person, bool>> e1 = c => (c.PId == 1) && (c.PName.Contains("k"));
            //Console.WriteLine(e1);

            //使用API构造Expression对象
            ParameterExpression param = Expression.Parameter(typeof (Person), "c");//c=>
            MemberExpression left1= Expression.Property(param, typeof (Person).GetProperty("PId"));//c.PId
            ConstantExpression right1= Expression.Constant(1);//1
            BinaryExpression e1 = Expression.Equal(left1, right1);//==

            MemberExpression left2 = Expression.Property(param, typeof (Person).GetProperty("PName"));
            ConstantExpression right2 = Expression.Constant("k");
            MethodCallExpression e2= Expression.Call(left2, typeof (string).GetMethod("Contains"), right2);

            Expression e3 = Expression.And(e1, e2);
            Console.WriteLine(e3);

            Console.ReadKey();

        }
    }
    public partial class Person
    {
     public    int PId { get; set; }
        public string PName { get; set; }
    }
}
