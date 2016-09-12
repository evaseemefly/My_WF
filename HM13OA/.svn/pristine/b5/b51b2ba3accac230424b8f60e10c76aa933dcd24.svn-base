using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HM13OA.IService;
using HM13OA.Model;

namespace HM13OA.UI.Controllers
{
    public class WorkflowModelController : MyBaseController//Controller
    {
        public IWorkFlowModelService WorkFlowModelService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPageList(int page,int rows)
        {
            int total;
            var result = WorkFlowModelService.GetPageList<int>(
                m => m.IsDelete == false,
                m => m.ModelId,
                rows, page, out total
                );
            return Json(new {total, rows = result}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(WorkFlowModel model)
        {
            string result = "no";

            //完善初始值
            model.IsDelete = false;
            model.SubBy = UserLogin.UserId;
            model.SubTime = DateTime.Now;
            if (model.Remark == null)
            {
                model.Remark = "";
            }

            //调用服务进行添加
            if (WorkFlowModelService.Add(model))
            {
                result = "ok";
            }

            return Content(result);
        }

        [HttpGet]
        public ActionResult Edit(int id1)
        {
            var m1 = WorkFlowModelService.GetById(id1);
            return View(m1);
        }
        [HttpPost]
        public ActionResult Edit(WorkFlowModel m1)
        {
            string result = "no";

            //完善信息
            m1.IsDelete = false;
            m1.SubBy = UserLogin.UserId;
            m1.SubTime = DateTime.Now;
            if (m1.Remark == null)
            {
                m1.Remark = "";
            }

            //调用服务，完成修改
            if (WorkFlowModelService.Edit(m1))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult Remove(string idList)
        {
            string result = "no";

            //整理数据
            List<int> list=new List<int>();
            string[] ids = idList.Split(',');
            foreach (var id in ids)
            {
                list.Add(int.Parse(id));
            }

            //调用服务，完成删除
            if (WorkFlowModelService.Remove(list.ToArray()))
            {
                result = "ok";
            }

            return Content(result);
        }
    }
}
