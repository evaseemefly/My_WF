using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lshOA.MODEL;

namespace lshOA.IBLL
{
    public partial interface IActionInfoBLL:IBaseBLL<ActionInfo>
    {
        bool SetActionRoleInfo(int actionId, List<int> list);
    }
}
