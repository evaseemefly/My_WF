using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HM13OA.Common;
using HM13OA.IService;
using HM13OA.Model;
using HM13OA.UI.Models;
using HM13OA.Workflow;
using Spring.Expressions.Parser.antlr.debug;

namespace HM13OA.UI.Controllers
{
    public class WorkflowController : MyBaseController//Controller
    {
        public IWorkFlowModelService WorkFlowModelService { get; set; }
        public IWorkflowInstanceService WorkflowInstanceService { get; set; }
        public IWorkflowStepService WorkflowStepService { get; set; }

        //列出所有可发起的流程
        public ActionResult Index()
        {
            var list = WorkFlowModelService.GetList(m => m.IsDelete == false);
            return View(list);
        }

        //列出所有待审批的流程
        public ActionResult ApproveList()
        {
            List<WorkflowStep> list= WorkflowStepService.GetList(
                m => 
                    (m.NextId == UserLogin.UserId)
                    &&
                    (m.IsOver==false))
                    .ToList();

            return View(list);
        }

        //发起报销流程的页面
        public ActionResult Baoxiao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Baoxiao(BaoxiaoInputFormModel m1)
        {
            string result = "no";

            //1、启动工作流
            Guid baoxiaoId= WorkflowHelper.Run(new Baoxiao(), new Dictionary<string, object>()
            {
                {"R2", m1.Reason},
                {"M2", m1.Money}
            });

            //2、创建工作流实例
            WorkflowInstance instance=new WorkflowInstance()
            {
                InstanceTitle =m1.BXTitle,
                InstanceGuid = baoxiaoId,
                InstanceState = (int)WorkFlowState.Approve,
                SubBy = UserLogin.UserId,
                SubTime = DateTime.Now,
            };

            //3、创建工作流步骤
            instance.WorkflowStep.Add(new WorkflowStep()
            {
                NextId = m1.NextId,
                SubBy = UserLogin.UserId,
                SubTime = DateTime.Now,
                Remark = string.Format("报销事由：{0},报销金额：{1}",m1.Reason,m1.Money)
            });

            //调用Service完成保存
            if (WorkflowInstanceService.Add(instance))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult BaoXiaoReinput(int id1)
        {
            ViewBag.InstanceId = id1;
            var list= WorkflowStepService.GetList(m => m.InstanceId == id1).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult BaoXiaoReinput(BaoxiaoReinputFormModel m1)
        {
            string result = "no";

            //0、前传：查找当前实例
            var instance = WorkflowInstanceService.GetById(m1.InstanceId);

            //1、继续工作流
            WorkflowHelper.Resume(new Baoxiao(),instance.InstanceGuid, "Input1",new BaoxiaoModel()
            {
                Reason = m1.Reason,
                Money = m1.Money
            });

            //2、更新当前状态
            instance.InstanceState = (int) WorkFlowState.Approve;

            //3、构造新步骤
            string remark = "事由：" + m1.Reason + "，金额：" + m1.Money + "，" + m1.Remark;
            instance.WorkflowStep.Add(new WorkflowStep()
            {
                IsOver = false,
                NextId = m1.NextId,
                Remark = remark,
                SubBy = UserLogin.UserId,
                SubTime = DateTime.Now
            });

            //4、更新数据
            if (WorkflowInstanceService.Approve(instance))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult BaoxiaoApprove(int id)
        {
            WorkflowStep step = WorkflowStepService.GetById(id);

            var instance = UserInfoService.GetById(step.SubBy);
            ViewBag.SubUserName = instance.UserName;

            return View(step);
        }
        [HttpPost]
        public ActionResult BaoxiaoApprove(bool BXApprove, string Remark, int NextId,int InstanceId,int StepId)
        {
            string result = "no";

            //0、前传：获取工作流实例对象
            WorkflowInstance instance = WorkflowInstanceService.GetById(InstanceId);

            //1、继续工作流
            WorkflowHelper.Resume(new Baoxiao(), instance.InstanceGuid, "Approve",BXApprove);

            //2、添加步骤
            if (Remark == null)
            {
                Remark = "";
            }
            if (!BXApprove)
            {
                Remark = "驳回，意见为：" + Remark;
            }
            

            WorkflowStep step=new WorkflowStep()
            {
                NextId = NextId,
                Remark = Remark,
                SubBy = UserLogin.UserId,
                SubTime = DateTime.Now,
                IsOver = NextId==0?true:false
            };
            instance.WorkflowStep.Add(step);

            //3、修改实例状态
            if (BXApprove && NextId == 0)
            {
                instance.InstanceState = (int) WorkFlowState.Over;
            }else if (!BXApprove)
            {
                instance.InstanceState = (int) WorkFlowState.Reject;
            }

            //4、修改当前步骤为完结
            WorkflowStepService.GetById(StepId).IsOver = true;
            
            //5、调用service完成保存
            if (WorkflowInstanceService.Approve(instance))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult GetPageList(int page, int rows)
        {
            int total;
            var result = WorkflowInstanceService.GetPageList<int>(
                m => m.SubBy == UserLogin.UserId,
                m => m.InstanceId,
               rows, page, out total)
               .Select(m=>new 
               {
                   m.InstanceId,
                   m.InstanceTitle,
                   m.InstanceState
               });
            return Json(new {total, rows = result}, JsonRequestBehavior.AllowGet);
        }

    }
}
