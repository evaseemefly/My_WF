
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HM13OA.DalFactory;
using HM13OA.IDAL;
using HM13OA.IService;
using HM13OA.Model;

namespace HM13OA.Service
{
        
    public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        protected override IBaseDal<UserInfo> GetDal()
        {
            return dbSession.GetUserInfoDal();
        }
	}
    public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        protected override IBaseDal<RoleInfo> GetDal()
        {
            return dbSession.GetRoleInfoDal();
        }
	}
    public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        protected override IBaseDal<ActionInfo> GetDal()
        {
            return dbSession.GetActionInfoDal();
        }
	}
    public partial class UserActionService:BaseService<UserAction>,IUserActionService
    {
        protected override IBaseDal<UserAction> GetDal()
        {
            return dbSession.GetUserActionDal();
        }
	}
    public partial class WorkFlowModelService:BaseService<WorkFlowModel>,IWorkFlowModelService
    {
        protected override IBaseDal<WorkFlowModel> GetDal()
        {
            return dbSession.GetWorkFlowModelDal();
        }
	}
    public partial class WorkflowInstanceService:BaseService<WorkflowInstance>,IWorkflowInstanceService
    {
        protected override IBaseDal<WorkflowInstance> GetDal()
        {
            return dbSession.GetWorkflowInstanceDal();
        }
	}
    public partial class WorkflowStepService:BaseService<WorkflowStep>,IWorkflowStepService
    {
        protected override IBaseDal<WorkflowStep> GetDal()
        {
            return dbSession.GetWorkflowStepDal();
        }
	}
}