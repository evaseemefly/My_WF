using System.Collections.Generic;
using System.Linq;
using lshOA.MODEL;
using lshOA.IBLL;

namespace lshOA.BLL
{
    public partial class RoleInfoBLL : BaseBLL<RoleInfo>, IRoleInfoBLL
    {
       
        /// <summary>
        /// 根据id软删除指定的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool LogicDelete(int roleId)
        {
            //1 根据id读取数据库获得用户对象实体
            var role = this.GetCurrentDBSession.RoleInfoDAL.GetList(u => u.ID == roleId).FirstOrDefault();
            //2 修改当前的用户实体对象的删除标记（改为软删除）
            role.DelFlag = (short)MODEL.Enum.DelFlagEnum.LogicDelete;
            //3 提交当前修改的用户实体
            CurrentDAL.Modify(role);
            //4 执行保存操作
           return this.GetCurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 根据传入的用户id集合批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool LogicDeleteByList(List<int> ids)
        {
            //1 找到所有传入id对应的实体对象
            var roleInfoList = this.GetCurrentDBSession.RoleInfoDAL.GetList(r => ids.Contains(r.ID));
            //2 遍历用户对象集合
            foreach (var roleInfo in roleInfoList)
            {
                //2.1 设置集合中的每个用户对象的修改标记符
                roleInfo.DelFlag = (short)MODEL.Enum.DelFlagEnum.LogicDelete;
                //2.2 将修改的数据提交到EF上下文中
                CurrentDAL.Modify(roleInfo);
            }
            //3 保存修改
           return this.GetCurrentDBSession.SaveChanges();
        }

        
    
    }
}
