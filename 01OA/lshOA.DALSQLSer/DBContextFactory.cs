using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace lshOA.DALSQLSer
{
    /// <summary>
    /// 使用线程内唯一 方法 为每个请求（每个线程中创建一个EF上下文对象
    /// </summary>
   public class DBContextFactory
    {
        /// <summary>
        /// 创建EF 上下文 对象
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDBContext()
        {
            /*
            注意：
            EF创建的数据上下文继承自DBContext
            */
            //1 获取当前线程中的EF上下文对象
            DbContext dbContext = CallContext.GetData(typeof(DBContextFactory).Name) as DbContext;
            //2 判断当前线程中 是否含EF上下文对象，若不存在则创建一个
            if(dbContext==null)
            {
                //2.1 将实体层的数据上下文对象赋给 EF上下文对象
                dbContext = new MODEL.OAEntities();
                //2.2 将已经获取到的EF上下文对象存储到当前线程中
                CallContext.SetData(typeof(DBContextFactory).Name, dbContext);
            }
            //3 返回数据上下文对象
            return dbContext;
        }
    }
}
