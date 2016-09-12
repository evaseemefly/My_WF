using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lshOA.IBLL;
using lshOA.MODEL;

namespace lshOA.BLL
{
    public partial class ActionInfoBLL :BaseBLL<ActionInfo>, IActionInfoBLL
    {
        /// <summary>
        /// 给权限分配角色
        /// </summary>
        /// <param name="actionId">权限id</param>
        /// <param name="list">角色id集合</param>
        /// <returns></returns>
       public bool SetActionRoleInfo(int actionId, List<int> list)
        {
            //1 根据权限id查询权限对象
            var actionInfo = this.GetCurrentDBSession.ActionInfoDAL.GetList(a => a.ID == actionId).FirstOrDefault();
            if(actionInfo!=null)
            {
                actionInfo.RoleInfo.Clear();
                foreach (var roleId in list)
                {
                    var roleInfo = this.GetCurrentDBSession.RoleInfoDAL.GetList(r => r.ID == roleId).FirstOrDefault();
                    actionInfo.RoleInfo.Add(roleInfo);
                }
            }
            return this.GetCurrentDBSession.SaveChanges();
        }
    }
}
