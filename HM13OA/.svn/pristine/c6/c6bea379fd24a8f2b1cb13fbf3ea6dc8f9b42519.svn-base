using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HM13OA.Common
{
    public partial class LogHelper
    {
        private static Queue<string> queue;
        public static void WriteLog(string msg)
        {
            //对写文件加锁，防止多个线程同时写文件时报异常
            //加锁问题：造成用户等待
            //lock ("a")
            //{
            //    File.AppendAllText(@"C:\Users\q1\Desktop\a.txt", msg);
            //}
            //解决等待：向内存中写，而不是硬盘中写,先写先读
            queue.Enqueue(msg);
        }

        static LogHelper()
        {
            queue = new Queue<string>(); 

            WriteDisk();
        }

        //定义一个方法，将内存中的错误消息写到硬盘上
        private static void WriteDisk()
        {
            Thread t=new Thread(() =>
            {
                while (true)
                {
                    if (queue.Count > 0)
                    {
                        string msg = queue.Dequeue();
                        File.AppendAllText(@"C:\Users\q1\Desktop\a.txt", msg+"\r\n---------------------------------\r\n");
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                }
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}
