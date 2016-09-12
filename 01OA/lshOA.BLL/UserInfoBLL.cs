using System.Collections.Generic;
using System.Linq;
using lshOA.MODEL;
using lshOA.IBLL;

namespace lshOA.BLL
{
    public partial class UserInfoBLL : BaseBLL<UserInfo>, IUserInfoBLL
    {
        //public override void SetCurrentDAL()
        //{
        //    this.CurrentDAL = this.GetCurrentDBSession.UserInfoDAL;
        //}

        /// <summary>
        /// 根据id软删除指定的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool LogicDelete(int userId)
        {
            //1 根据id读取数据库获得用户对象实体
            var user = this.GetCurrentDBSession.UserInfoDAL.GetList(u => u.ID == userId).FirstOrDefault();
            //2 修改当前的用户实体对象的删除标记（改为软删除）
            user.DelFlag = (short)MODEL.Enum.DelFlagEnum.LogicDelete;
            //3 提交当前修改的用户实体
            CurrentDAL.Modify(user);
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
            var userInfoList = this.GetCurrentDBSession.UserInfoDAL.GetList(u => ids.Contains(u.ID));
            //2 遍历用户对象集合
            foreach (var userInfo in userInfoList)
            {
                //2.1 设置集合中的每个用户对象的修改标记符
                userInfo.DelFlag = (short)MODEL.Enum.DelFlagEnum.LogicDelete;
                //2.2 将修改的数据提交到EF上下文中
                CurrentDAL.Modify(userInfo);
            }
            //3 保存修改
           return this.GetCurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 多条件搜索
        /// </summary>
        /// <param name="userInfoParam"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> GetSearchList(MODEL.SearchParam.UserInfoParam userInfoParam)
        {
            short delFlag = (short)MODEL.Enum.DelFlagEnum.Normarl;
            var temp = this.GetCurrentDBSession.UserInfoDAL.GetList(u => u.DelFlag == delFlag);
            //若传入匹配的名字
            if(!string.IsNullOrEmpty(userInfoParam.UserName))
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoParam.UserName));
            }
            if(!string.IsNullOrEmpty(userInfoParam.Remark))
            {
                temp = temp.Where<UserInfo>(u => u.Remark.Contains(userInfoParam.Remark));

            }

            userInfoParam.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoParam.PageIndex - 1) * userInfoParam.PageSize).Take<UserInfo>(userInfoParam.PageSize);
        }

        /// <summary>
        /// 根据 userID与actionID 修改当前的 R_UserInfo_ActionInfo 关系表中的对应行
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="actionId"></param>
        /// <param name="value">isPass对应的值</param>
        /// <returns></returns>
        public bool SetUserAction(int userId, int actionId, bool value)
        {
            //根据传入的用户id与权限id 查询 R_UserInfo_ActionInfoDAL 是否包含此记录
            var actionInfo= this.GetCurrentDBSession.R_UserInfo_ActionInfoDAL.GetList(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
            //若不含此关系则创建此关系
            if (actionInfo==null)
            {
                R_UserInfo_ActionInfo r_userInfo_ActionInfo = new R_UserInfo_ActionInfo();
                r_userInfo_ActionInfo.IsPass = value;
                r_userInfo_ActionInfo.ActionInfoID = actionId;
                r_userInfo_ActionInfo.UserInfoID = userId;
                this.GetCurrentDBSession.R_UserInfo_ActionInfoDAL.Add(r_userInfo_ActionInfo);
              
            }
            //若已经包含此关系，则修改此关系的 isPass 的值
            else
            {
                //若传入的isPass的值与当前数据库中保存的不相等，则修改为新的isPass
                if(actionInfo.IsPass!=value)
                {
                    actionInfo.IsPass = value;
                    //return this.GetCurrentDBSession.SaveChanges();
                }
            }
            return this.GetCurrentDBSession.SaveChanges();
        }

    }
}
