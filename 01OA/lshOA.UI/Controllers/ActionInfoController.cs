using lshOA.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace lshOA.UI.Controllers
{
    public class ActionInfoController : BaseController
    {
        /// <summary>
        /// 通过spring .net为其赋值
        /// </summary>
        IBLL.IActionInfoBLL actionInfoBLL { get; set; }

        IBLL.IRoleInfoBLL roleInfoBLL { get; set; }
        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取权限信息列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string name = Request["name"];
            string remark = Request["remark"];
            int totalCount = 0;
            short delFlag = (short)lshOA.MODEL.Enum.DelFlagEnum.Normarl;
            var actionInfoList = actionInfoBLL.GetPageList<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            //
            var temp = from u in actionInfoList
                       select new { ID = u.ID, ActionInfoName = u.ActionInfoName, Url = u.Url, HttpMethod = u.HttpMethod, ActionTypeEnum = u.ActionTypeEnum ,Sort = u.Sort, Remark = u.Remark, SubTime = u.SubTime };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据传入的权限实体添加权限信息到数据库
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.SubTime = DateTime.Now;
            actionInfo.ModifiedOn = DateTime.Now;
            actionInfo.DelFlag = (short)MODEL.Enum.DelFlagEnum.Normarl;
            actionInfoBLL.Add(actionInfo);
            return Content("ok");
        }
        /// <summary>
        /// 展示修改权限视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowEditInfo(int id)
        {
            //也可以通过以下的方式获取id
            //int.Parse(Request["id"]);
            //由于返回的强类型，故页面应该使用强类型视图
            ViewData.Model = actionInfoBLL.GetList(a => a.ID == id).FirstOrDefault();
            return View();
        }

        /// <summary>
        /// 根据传入的去权限进行修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction(ActionInfo actionInfo)
        {
            actionInfo.ModifiedOn = DateTime.Now;
            actionInfoBLL.Modify(actionInfo);
            return Content("ok");
        }

        /// <summary>
        /// 根据提交的文件，将图片上传至服务器，成功的话将文件在服务器另存为的名称返回，并返回状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuIcon()
        {
            string fileName = null;
            string fileNewPath = null;
            //创建一个对象用来保存状态及最终返回的图片地址
            object returnObj =null;
            if (Request.Files.Count == 0)
            {
                returnObj = new { state = "no", errorMessage = "上传的文件个数为0", imgSerPath = "" }; 
            }
            else
            {
                HttpPostedFileBase file = Request.Files[0];
                fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName).ToLower();
                
                //判断后缀是否为.png或者为.jpg
                if (fileExt == ".png" || fileExt == ".jpg")
                {
                    string newfileName = Guid.NewGuid().ToString() + fileExt;
                    fileNewPath = "/MenuIcon/" + newfileName;
                    file.SaveAs(Server.MapPath(fileNewPath));//将图片保存至新的路径
                    //return Content("ok:/MenuIcon/" + newfileName);
                    returnObj = new { state = "ok", errorMessage = "", imgSerPath = fileNewPath };

                }
                else
                {
                    returnObj = new { state = "error", errorMessage = "上传文件格式错误", imgSerPath =""};
                    //return Content("no");
                }
            }
            return Json(returnObj, "text/html", JsonRequestBehavior.AllowGet);
          
        }

        /// <summary>
        /// 根据传入的id查询权限，
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetActionRole(int id)
        {
            //获取权限对象
            var actionInfo = actionInfoBLL.GetList(a => a.ID == id).FirstOrDefault();

            //将权限对象存入ViewBag中，并返回
            ViewBag.ActionInfo = actionInfo;
            short delFlag = (short)MODEL.Enum.DelFlagEnum.Normarl;
            //获取全部角色集合
            ViewBag.AllRoles= roleInfoBLL.GetList(r => r.DelFlag == delFlag).ToList();
            //获取当前权限 所拥有的 角色集合
            ViewBag.AllExtRoleIds = (from r in actionInfo.RoleInfo  //RoleInfo是ActionInfo的一个导航属性 此处是一对多的关系（即一个ActionInfo——权限 对应多个RoleInfo——角色，一个角色可能有多个权限）
                                     select r.ID).ToList();
            return View();
            
        }

        [HttpPost]
        public ActionResult SetActionRole(FormCollection collection)
        {
            int actionId = int.Parse(Request["actionId"]);
            string[] AllKeys = Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach (var item in AllKeys)
            {
                if (item.StartsWith("cba_"))
                {
                    string key = item.Replace("cba_", "");
                    list.Add(int.Parse(key));
                }
            }
            actionInfoBLL.SetActionRoleInfo(actionId, list);
            return Content("ok");
        }
    }
}