using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using lshOA.IDAL;

namespace lshOA.DALFactory
{
    public partial class AbstractFactory
    {
     
        //lshOA.DALSQLSer
        //类库名称
        private static readonly string DALAssemblyPath = ConfigurationManager.AppSettings["DALAssemblyPath"];

        //lshOA.DALSQLSer
        //命名空间
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];

        
        /// <summary>
        /// 创建 UserInfo的实例
        /// </summary>
        /// <returns></returns>
        //public static IUserInfoDAL CreateUserInfoDAL()
        //{
        //    //获取类的全名称：命名空间+类名
        //    //使用反射创建注意需要添加该类库的引用
        //    string fullClassName = NameSpace + ".UserInfoDAL";
        //   return CreateInstance(fullClassName) as IUserInfoDAL;            
        //}

        /// <summary>
        /// 反射创建对象实例
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static object CreateInstance(string fullClassName)
        {
            //类库名称（加载程序集）
            var assembly = Assembly.Load(DALAssemblyPath);
            //根据类的全名称 反射创建对象实例
           return assembly.CreateInstance(fullClassName);
        }
    }
}
