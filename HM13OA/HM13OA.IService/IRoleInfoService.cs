using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.IService
{
    public partial interface IRoleInfoService
    {
        bool SetAction(int rid,string aids);
    }
}
