using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lshOA.MODEL;

namespace lshOA.UI.Controllers
{
    public class UserInfoController : BaseController
    {
        //IBLL.IUserInfoBLL userInfoBLL = new BLL.UserInfoBLL();
        //改为属性，通过spring .net为其赋值
        IBLL.IUserInfoBLL userInfoBLL { get; set; }

        /// <summary>
        /// 权限属性，由是spring.net为其赋值
        /// </summary>
        IBLL.IActionInfoBLL actionInfoBLL { get; set; }


        IBLL.IR_UserInfo_ActionInfoBLL r_UserInfo_ActionInfoBLL { get; set; }

        // GET: User
        public ActionResult Index()
        {
            //var userInfo = userInfoBLL.GetList(u => u.DelFlag == 0);
            //ViewData.Model = userInfo;
            //return View();
            return View();
        }

        public ActionResult GetUserInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string name = Request["name"];
            string remark = Request["remark"];
            int totalCount=0;

            MODEL.SearchParam.UserInfoParam userInfoParam = new MODEL.SearchParam.UserInfoParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                UserName = name,
                Remark = remark

            };
            ////未删除的标记符
            //short deleteType = (short)MODEL.Enum.DelFlagEnum.Normarl;
            ////根据查询条件及排序条件插叙人员信息集合
            //var userInfoList= userInfoBLL.GetPageList<string>(pageIndex, pageSize, out totalCount, u => u.DelFlag == deleteType, u=>u.Sort,true);
            var userInfoList = userInfoBLL.GetSearchList(userInfoParam);
            //
            var temp = from u in userInfoList
                       select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd, Remark = u.Remark, SubTime = u.SubTime,DelFlag=u.DelFlag,Sort=u.Sort};
            return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
            
        }

        //public ActionResult EditUserInfo()
        //{
            
        //}

        /// <summary>
        /// 根据传入的id数据，执行软删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteUserInfo()
        {
            //1 获取请求的报文
            string strId = Request["strId"];
            //2 根据，分割id
            string[] Ids = strId.Split(',');
            //IEnumerable<int> list= Ids.ToList<int>();
            //3 获取id集合
            List<int> list = new List<int>();
            foreach (var id in Ids)
            {
                list.Add(int.Parse(id));
            }
            //4 删除成功标记
            bool isDel = false;
            //判断若传入的是一个id则执行删除单个的方法，若传入的是一组id则执行批量删除的方法
            if (list.Count==1)
            {
               isDel= userInfoBLL.LogicDelete(list.FirstOrDefault());
            }
            else if(Ids.Count() > 1)
            {
               isDel= userInfoBLL.LogicDeleteByList(list);
            }
            //若删除成功返回ok
            if(isDel)
            {
                return Content("ok");
            }
            //删除失败返回err
            else if(!isDel)
            {
                return Content("err");
            }
            return View();
        }

        /// <summary>
        /// 新增人员信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            //对提交的数据进行验证
            if(userInfo==null)
            {
                return Content("err");
            }
            else
            {
                userInfo.SubTime = DateTime.Now;
                userInfo.ModifiedOn = DateTime.Now;
                userInfo.DelFlag = (short)MODEL.Enum.DelFlagEnum.Normarl;//删除标记为未删除
                userInfoBLL.Add(userInfo);
                return Content("ok");
            }
           
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            
            if(userInfoBLL.Modify(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("err");
            }
        }

        /// <summary>
        /// 为用户分配权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetUserActionInfo(/*int id*/)
        {
            //根据传入的id获取对应的用户对象
            if(userInfoBLL==null)
            { }
            else
            {
                int id = int.Parse(Request["id"]);
                var userInfo = userInfoBLL.GetList(u => u.ID == id).FirstOrDefault();
                ViewData.Model = userInfo;
                //将该用户对象存入ViewBag中
                ViewBag.UserInfo = userInfo;
                //查询所有的权限
                ViewBag.AllActions = actionInfoBLL.GetList(a => a.DelFlag == 0).ToList();
                //查找当前用户的所拥有的权限
                //查找当前用户的所有的权限（导航属性）
                ViewBag.AllExtActions = userInfo.R_UserInfo_ActionInfo.ToList();
            }
           
            return View();
        }

        /// <summary>
        /// 根据 userId以及actionId 修改（或创建）对应的关系
        /// </summary>
        /// <returns></returns>
        public ActionResult SetActionForUser()
        {
            //获取请求中的三个参数
            int userid = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
            bool value = Request["value"] == "true" ? true : false;
            if (userInfoBLL.SetUserAction(userid, actionId, value))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }

        /// <summary>
        /// 清除传入的 指定的User的 特定的action（特性）
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearActionUser()
        {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
            var actionInfo = r_UserInfo_ActionInfoBLL.GetList(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
            if(actionInfo!=null)
            {
                r_UserInfo_ActionInfoBLL.Del(actionInfo);
                return Content("ok");
            }
            return Content("no");
        }

    }
}