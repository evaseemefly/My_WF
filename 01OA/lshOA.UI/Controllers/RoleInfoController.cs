using lshOA.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Controllers
{
    public class RoleInfoController : BaseController
    {
        IBLL.IRoleInfoBLL roleInfoBLL { get; set; }
        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string name = Request["name"];
            string remark = Request["remark"];
            int totalCount = 0;
            short delFlag = (short)lshOA.MODEL.Enum.DelFlagEnum.Normarl;
            var roleInfoList = roleInfoBLL.GetPageList<int>(pageIndex, pageSize,out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            //
            var temp = from u in roleInfoList
                       select new { ID = u.ID, RoleName = u.RoleName, Sort = u.Sort, Remark = u.Remark, SubTime = u.SubTime };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = (short)MODEL.Enum.DelFlagEnum.Normarl;//删除标记为未删除;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfoBLL.Add(roleInfo);
            return Content("ok");
        }

        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            ViewData.Model = roleInfoBLL.GetList(r => r.ID == id).FirstOrDefault();
            return View();
        }

        public ActionResult EditRoleInfo(RoleInfo roleInfo)
        {
            //由于提交过来的角色实体中的删除标记并未被赋值，且通过本方式提交过来的roleInfo肯定是未被修改的
            //判断其中的删除属性是否为空
            //if(roleInfo.DelFlag==null)
            //{

            //}
            if(roleInfoBLL.Modify(roleInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult DeleteUserInfo()
        {
            //1 接收传过来的id
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(int.Parse(id));
            }
            if (roleInfoBLL.LogicDeleteByList(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }

    }
}