﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM13OA.Model;

namespace HM13OA.IService
{
    public partial interface IWorkflowInstanceService
    {
        bool Approve(WorkflowInstance instance);
    }
}
