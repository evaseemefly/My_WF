using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lshOA.MODEL;
using Spring.Context;
using Spring.Context.Support;

namespace lshOA.UI.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            bool isExt = false;
            //若当前cookie中有保存服务器的sessionId
            if(Request.Cookies["sessionId"]!=null)
            {
                //1 判断当前用户是否登录
                //获取sessionId（memcache）
                string sessionId = Request.Cookies["sessionId"].Value;
                //根据key从memcache中获取用户信息
                object obj = COMMON.MemecacheHelper.Get(sessionId);

                if(obj!=null)
                {
                    UserInfo userInfo = COMMON.SerializerHelper.DeSerializerToObject<UserInfo>(obj.ToString());
                    LoginUser = userInfo;
                    isExt = true;

                    //留一个后门 
                    if(LoginUser.UName=="itcast")
                    {
                        return;
                    }

                    //2 完成权限过滤
                    //取得请求的url地址与请求方式
                    string requestUrl = Request.Url.AbsolutePath.ToLower();
                    string requestHttpMethod = Request.HttpMethod.ToLower();    //post,get
                    //?通过Spring.net的IOC容器IApplicationContext从config文件中取得程序集信息和调用方法实现控制反转
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    lshOA.IBLL.IUserInfoBLL userInfoBLL = (lshOA.IBLL.IUserInfoBLL)ctx.GetObject("userInfoService");

                    IBLL.IActionInfoBLL actionBLL = (IBLL.IActionInfoBLL)ctx.GetObject("actionInfoService");

                    //根据url地址以及请求方式找到具体的权限
                    var currentAction= actionBLL.GetList(a => a.Url.ToLower() == requestUrl && a.HttpMethod.ToLower() == requestHttpMethod).FirstOrDefault();
                    if(currentAction==null)
                    {
                        Response.Redirect("/Error.html");
                        //return;
                    }
                    /*
                    第一种方式
                    User - R_UserInfo_ActionInfo - Action
                    1 先获取当前登录用户
                    */
                    //1 获取当前登录用户对象
                    //根据当前保存在session中的UserInfo对象，获取其id，并根据其id查询数据库中是否包含此用户
                    var currentUserInfo = userInfoBLL.GetList(u => u.ID == LoginUser.ID).FirstOrDefault();
                    if (currentUserInfo != null)
                    {
                        //数据库中存在此UserInfo对象
                        //获取当前用户包含的R_UserInfo_ActionInfo 中所对应的ActionInfo是否与当前请求提交的ActionInfo相同
                       var actions= currentUserInfo.R_UserInfo_ActionInfo.Where(r => r.ActionInfoID == currentAction.ID).FirstOrDefault();
                        if(actions!=null)
                        {
                            if(actions.IsPass==true)
                            {
                                return;
                            }
                            else
                            {
                                Response.Redirect("/Error.html");
                            }
                        }

                        /*
                         第二种方式
                         User - Role - Action
                        1 先获取当前登录用户
                         */
                        //查找当前用户包含的Role
                        var roles = currentUserInfo.RoleInfo;
                        //查找roles中的所有action
                        var currentUserActions = from a in roles
                                                 select a.ActionInfo;
                        //对于所有的action，判断其Id与当前currentAction的Id是否相等，找出相等的action
                        var count = (from a in currentUserActions
                                     from b in a
                                     where b.ID == currentAction.ID
                                     select b).Count();
                        if(count<1)
                        {
                            Response.Redirect("/Error.html");
                            return;
                        }

                        //foreach (var role in roles)
                        //{
                        //   if(role.ActionInfo.Contains(currentAction))
                        //    { return; }
                        //}
                        //Response.Redirect("/Error.html");
                    }



                }

            }
            if(!isExt)
            {
                filterContext.HttpContext.Response.Redirect("/login/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
   
    }
}