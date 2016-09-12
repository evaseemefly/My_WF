using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Controllers
{
    public class WF_InstanceController : BaseController
    {
        IBLL.IWF_TempBLL wf_TempBLL { get; set; }
        IBLL.IUserInfoBLL userInfoBLL { get; set; }
        // GET: WF_Instance

            /// <summary>
            /// 获取模板列表
            /// </summary>
            /// <returns></returns>
        public ActionResult Index()
        {
            short DelFlag = (short)lshOA.MODEL.Enum.DelFlagEnum.Normarl;
            ViewData.Model = wf_TempBLL.GetList(c => c.DelFlag == DelFlag).ToList();
            return View();
        }

        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StartWorkflow(int id)
        {
            //获取当前id对应的模板
            var currentTemp = wf_TempBLL.GetList(w => w.ID == id).FirstOrDefault();
            //
            ViewBag.Temp = currentTemp;
            var userInfoList = from u in userInfoBLL.GetList(u => u.DelFlag == 0).ToList()
                               select new SelectListItem()
                               {
                                   Selected = false,
                                   Text = u.UName,
                                   Value = u.ID.ToString()
                               };
            ViewData["FlowTo"] = userInfoList;//如果ViewData中的名字与DropDownList名字一致会自动填充
            return View();
        }
    }
}