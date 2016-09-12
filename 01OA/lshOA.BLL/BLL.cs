
 
using System.Collections.Generic;
using System.Linq;
using lshOA.MODEL;
using lshOA.IBLL;

namespace lshOA.BLL
{
	public partial class ActionInfoBLL : BaseBLL<ActionInfo>, IActionInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.ActionInfoDAL;
        }
	}  

	public partial class BookBLL : BaseBLL<Book>, IBookBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.BookDAL;
        }
	}  

	public partial class DepartmentBLL : BaseBLL<Department>, IDepartmentBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.DepartmentDAL;
        }
	}  

	public partial class FileInfoBLL : BaseBLL<FileInfo>, IFileInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.FileInfoDAL;
        }
	}  

	public partial class OrderInfoBLL : BaseBLL<OrderInfo>, IOrderInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.OrderInfoDAL;
        }
	}  

	public partial class R_UserInfo_ActionInfoBLL : BaseBLL<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.R_UserInfo_ActionInfoDAL;
        }
	}  

	public partial class RoleInfoBLL : BaseBLL<RoleInfo>, IRoleInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.RoleInfoDAL;
        }
	}  

	public partial class sysdiagramsBLL : BaseBLL<sysdiagrams>, IsysdiagramsBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.sysdiagramsDAL;
        }
	}  

	public partial class UserInfoBLL : BaseBLL<UserInfo>, IUserInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.UserInfoDAL;
        }
	}  

	public partial class WF_InstanceBLL : BaseBLL<WF_Instance>, IWF_InstanceBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.WF_InstanceDAL;
        }
	}  

	public partial class WF_StepInfoBLL : BaseBLL<WF_StepInfo>, IWF_StepInfoBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.WF_StepInfoDAL;
        }
	}  

	public partial class WF_TempBLL : BaseBLL<WF_Temp>, IWF_TempBLL
    {
		public override void SetCurrentDAL()
        {
            this.CurrentDAL = this.GetCurrentDBSession.WF_TempDAL;
        }
	}  


}