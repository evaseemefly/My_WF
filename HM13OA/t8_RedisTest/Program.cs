using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace t8_RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisClient client=new RedisClient("192.168.1.100",6379);
            client.AddItemToSortedSet("xh", "jk123", 3);
            client.AddItemToSortedSet("xh", "xh789", 6);
            client.AddItemToSortedSet("xh", "nll456", 2);

            //List<string> list = client.GetAllItemsFromSortedSetDesc("xh");
            List<string> list = client.GetAllItemsFromSortedSet("xh");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
