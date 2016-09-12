﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.IDAL
{
    public partial interface IDbSession
    {
		IUserInfoDal GetUserInfoDal();
		IRoleInfoDal GetRoleInfoDal();
		IActionInfoDal GetActionInfoDal();
		IUserActionDal GetUserActionDal();
		IWorkFlowModelDal GetWorkFlowModelDal();
		IWorkflowInstanceDal GetWorkflowInstanceDal();
		IWorkflowStepDal GetWorkflowStepDal();
	}
}