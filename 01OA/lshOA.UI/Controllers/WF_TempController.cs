using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lshOA.MODEL;

namespace lshOA.UI.Controllers
{
    /// <summary>
    /// 流程模板
    /// </summary>
    public class WF_TempController : BaseController
    {
        IBLL.IWF_TempBLL wf_TempBLL { get; set; }
        // GET: WF_Temp
        public ActionResult Index()
        {
            short DelFlag =(short) lshOA.MODEL.Enum.DelFlagEnum.Normarl;
            var wf_TempList = wf_TempBLL.GetList(w => w.DelFlag == DelFlag).ToList();
            ViewData["list"] = wf_TempList;
            return View();
        }

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WF_Temp wf_Temp)
        {
            wf_Temp.DelFlag = 0;
            wf_Temp.ModfiedOn = DateTime.Now;
            wf_Temp.Remark = "财务审批的流程模板";
            wf_Temp.SubBy = LoginUser.ID;   //因为继承自BaseController，可以获取到登录的用户
            wf_Temp.SubTime = DateTime.Now;
            wf_Temp.TempStatus = 0;
            wf_TempBLL.Add(wf_Temp);
            return RedirectToAction("Index");
        }
    }
}