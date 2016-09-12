﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM13OA.Model;

namespace HM13OA.IService
{
        
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
    }
    public partial interface IRoleInfoService:IBaseService<RoleInfo>
    {
    }
    public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
    }
    public partial interface IUserActionService:IBaseService<UserAction>
    {
    }
    public partial interface IWorkFlowModelService:IBaseService<WorkFlowModel>
    {
    }
    public partial interface IWorkflowInstanceService:IBaseService<WorkflowInstance>
    {
    }
    public partial interface IWorkflowStepService:IBaseService<WorkflowStep>
    {
    }
}