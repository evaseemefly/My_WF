using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached;
using Memcached.ClientLibrary;

namespace lshOA.COMMON
{
    public class MemecacheHelper
    {
        /// <summary>
        /// memcache 客户端实例
        /// </summary>
        private static readonly Memcached.ClientLibrary.MemcachedClient mc = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        static MemecacheHelper()
        {
            string[] serverlist = { "192.168.1.100:11211", "192.168.1.106:11211" };//一定要将地址写到Web.config文件中。
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();

            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // 获得客户端实例
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 向memcache中写入数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key,object value)
        {
            mc.Set(key, value);
        }

        /// <summary>
        /// 向memcache中写入数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt">过期时间</param>
        public static void Set(string key,object value,DateTime dt)
        {
            mc.Set(key, value, dt);
        }

        public static object Get(string key)
        {
          return  mc.Get(key);
        }

        public static bool Delete(string key)
        {
            //1 判断是否存在指定key对应的缓存
            if(mc.KeyExists(key))
            {
               return mc.Delete(key);
            }
            else
            {
                return false;
            }
            
        }
    }
}
