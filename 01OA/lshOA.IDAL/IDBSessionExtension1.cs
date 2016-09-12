
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.IDAL
{
    public partial interface IDBSession
    {
           
        IActionInfoDAL ActionInfoDAL { get; set; }

		       
        IBookDAL BookDAL { get; set; }

		       
        IDepartmentDAL DepartmentDAL { get; set; }

		       
        IFileInfoDAL FileInfoDAL { get; set; }

		       
        IOrderInfoDAL OrderInfoDAL { get; set; }

		       
        IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL { get; set; }

		       
        IRoleInfoDAL RoleInfoDAL { get; set; }

		       
        IsysdiagramsDAL sysdiagramsDAL { get; set; }

		       
        IUserInfoDAL UserInfoDAL { get; set; }

		       
        IWF_InstanceDAL WF_InstanceDAL { get; set; }

		       
        IWF_StepInfoDAL WF_StepInfoDAL { get; set; }

		       
        IWF_TempDAL WF_TempDAL { get; set; }

		    }
}