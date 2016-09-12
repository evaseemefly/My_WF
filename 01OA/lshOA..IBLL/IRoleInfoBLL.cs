using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lshOA.MODEL;

namespace lshOA.IBLL
{
    public partial interface IRoleInfoBLL:IBaseBLL<RoleInfo>
    {
        bool LogicDelete(int userId);

        bool LogicDeleteByList(List<int> list);
    }
}
