﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM13OA.Model;

namespace HM13OA.IDAL
{
    public partial interface IUserInfoDal//:IBaseDal<UserInfo>
    {
        int SetRole(int uid, int[] rids);
        int SetAction(int uid, int aid, int allow);
    }
}
