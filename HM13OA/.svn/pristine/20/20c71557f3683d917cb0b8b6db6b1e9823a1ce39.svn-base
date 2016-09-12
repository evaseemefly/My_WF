using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;

namespace t5_mmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //架设服务器集群
            string[] ips=new string[]
            {
                "192.168.1.100:11211",
                "192.168.1.67:11211"
            };

            //通信池的初始化
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(ips);
            pool.Initialize();

            //创建客户端对象，完成通信
         MemcachedClient client=new MemcachedClient();
            client.EnableCompression = false;
            client.Set("zxh", 123, 600);

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
