﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.COMMON
{
   public class WebCommon
    {
        public static string MD5String(string str)
        {
            //1 创建MD5的实现类
            MD5 md5 = MD5.Create();
            //2 将字符串编码为一个字节序列
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            //3 计算哈希值
            byte[] md5Buffer = md5.ComputeHash(buffer);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            
            }
            md5.Clear();
            return sb.ToString();
        }
    }
}
