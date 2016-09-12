using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lshOA.MODEL;

namespace lshOA.IBLL
{
    public partial interface IUserInfoBLL:IBaseBLL<UserInfo>
    {
        bool LogicDelete(int userId);

        bool LogicDeleteByList(List<int> list);

        IQueryable<UserInfo> GetSearchList(MODEL.SearchParam.UserInfoParam userInfoParam);

        bool SetUserAction(int userId, int actionId, bool value);
    }
}
