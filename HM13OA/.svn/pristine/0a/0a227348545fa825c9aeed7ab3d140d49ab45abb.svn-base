using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM13OA.IDAL;

namespace HM13OA.Service
{
    public partial class RoleInfoService
    {
        public bool SetAction(int rid, string aids)
        {
            List<int> list1=new List<int>();
            string[] list2 = aids.Split(',');
            foreach (var item in list2)
            {
                list1.Add(int.Parse(item));
            }

            ((IRoleInfoDal) curDal).SetAction(rid, list1.ToArray());

            return dbSession.SaveChanges() > 0;
        }
    }
}
