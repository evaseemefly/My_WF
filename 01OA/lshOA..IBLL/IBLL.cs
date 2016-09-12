
 using System.Configuration;
using System.Reflection;
using lshOA.IDAL;
using lshOA.MODEL;

namespace lshOA.IBLL
{
	public partial interface IActionInfoBLL:IBaseBLL<ActionInfo>
     { }		
		public partial interface IBookBLL:IBaseBLL<Book>
     { }		
		public partial interface IDepartmentBLL:IBaseBLL<Department>
     { }		
		public partial interface IFileInfoBLL:IBaseBLL<FileInfo>
     { }		
		public partial interface IOrderInfoBLL:IBaseBLL<OrderInfo>
     { }		
		public partial interface IR_UserInfo_ActionInfoBLL:IBaseBLL<R_UserInfo_ActionInfo>
     { }		
		public partial interface IRoleInfoBLL:IBaseBLL<RoleInfo>
     { }		
		public partial interface IsysdiagramsBLL:IBaseBLL<sysdiagrams>
     { }		
		public partial interface IUserInfoBLL:IBaseBLL<UserInfo>
     { }		
		public partial interface IWF_InstanceBLL:IBaseBLL<WF_Instance>
     { }		
		public partial interface IWF_StepInfoBLL:IBaseBLL<WF_StepInfo>
     { }		
		public partial interface IWF_TempBLL:IBaseBLL<WF_Temp>
     { }		
	}