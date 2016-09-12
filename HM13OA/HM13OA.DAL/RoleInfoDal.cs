using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.DAL
{
    public partial class RoleInfoDal
    {
        public int SetAction(int rid, int[] aids)
        {
            var roleInfo = GetById(rid);
            
            roleInfo.ActionInfo.Clear();

            ActionInfoDal actionInfoDal=new ActionInfoDal();
            foreach (var aid in aids)
            {
                roleInfo.ActionInfo.Add(actionInfoDal.GetById(aid));
            }

            return 0;
        }
    }
}
