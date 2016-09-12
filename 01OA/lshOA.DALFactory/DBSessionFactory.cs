using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.DALFactory
{
    public class DBSessionFactory
    {
        /// <summary>
        /// 使用 线程内唯一 的方法创建DBSession
        /// </summary>
        /// <returns></returns>
        public static IDAL.IDBSession CreateDbSession()
        {
            //从线程中获取DBSession
            IDAL.IDBSession DbSession = CallContext.GetData(typeof(DBSessionFactory).Name) as IDAL.IDBSession;
            //若没有则创建DBSession
            if(DbSession==null)
            {
                DbSession = new DALFactory.DBSession();
                CallContext.SetData(typeof(DBSessionFactory).Name, DbSession);
            }
            //返回DBSession
            return DbSession;
        }
    }
}
