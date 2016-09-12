﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM13OA.Model;

namespace HM13OA.IService
{
    public partial interface IUserInfoService//:IBaseService<UserInfo>
    {
        bool Login(UserInfo userInfo,out int id1);
        bool SetRole(int uid, string rids);
        bool SetAction(int uid, int aid, int allow);
    }
}
