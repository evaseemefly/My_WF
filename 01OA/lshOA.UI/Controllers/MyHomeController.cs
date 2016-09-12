using lshOA.MODEL.Enum;
using lshOA.MODEL.ActionEqualityCompare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Controllers
{
    //暂时不使用登录验证所以先继承自controller
    public class MyHomeController :/*Controller*/ BaseController
    {
        IBLL.IUserInfoBLL userInfoBLL { get; set; }
        // GET: MyHome
        public ActionResult Index()
        {

            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.UName;
            }
            return View();
        }

        public ActionResult GetMenuItems()
        {
            //1 获取当前用户对象
            var userInfo = userInfoBLL.GetList(u => u.ID == LoginUser.ID).FirstOrDefault();
            // 查询用户已经拥有的角色
            var userRoles = userInfo.RoleInfo;

            //2 找出用户已经拥有的角色对应的权限
            short menuType = (short)ActionTypeEnum.MenuActionType;
            /*
            -1 遍历userRoles（已经拥有的角色）
            -2 遍历userRoles中的ActionInfo
               判断ActionInfo中的ActionTypeEnum是否为菜单权限
            */
            var userMenuItem = (from r in userRoles
                                from a in r.ActionInfo
                                where a.ActionTypeEnum == menuType
                                select a).ToList();

            //3 找出用户特有的权限
            var userActions = userInfo.R_UserInfo_ActionInfo.ToList();
            var isPassUserActions = from a in userActions
                                    where a.IsPass == true && a.ActionInfo.ActionTypeEnum == menuType
                                    select a;
            var isPassActions = (from a in isPassUserActions
                                 select a.ActionInfo).ToList();

            //合并两个ActionInfo集合
            userMenuItem.AddRange(isPassActions);

            //找出禁止权限
            var isNotPassUserActions = (from a in userActions
                                        where a.IsPass == false
                                        select a.ActionInfoID).ToList();
            //完成禁用权限的过滤
            userMenuItem = userMenuItem.Where(a => !isNotPassUserActions.Contains(a.ID)).ToList();
            //此时集合中还有重复的
           userMenuItem= userMenuItem.Distinct(new ActionEqualCompare()).ToList();

            //{ icon: '/Content/ligerUI/images/3DSMAX.png', title: '用户管理', url: '/UserInfo/Index' }
            var result = from u in userMenuItem
                         select new
                         {
                             icon = u.MenuIcon,
                             title = u.ActionInfoName,
                             url = u.Url
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}