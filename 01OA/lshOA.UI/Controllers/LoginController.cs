using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lshOA.MODEL;

namespace lshOA.UI.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 由spring .net 为其赋值
        /// </summary>
        IBLL.IUserInfoBLL userInfoBLL { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            //在每次跳转至登录页的时候判断是否保存的了当前用户的登录状态
            CheckCookieInfo();
            return View();
        }

        /// <summary>
        /// 展示验证码并加验证码中的字符串写入Session
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
            COMMON.ValidateCode validateCode = new COMMON.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "imge.jepg");
        }

        public ActionResult Logout()
        {
            //当前请求中的cookie中是否包含sessionId
            if(Request.Cookies["sessionId"]!=null)
            {
                string key = Request.Cookies["sessionId"].Value;
                //根据key删除当前memecache中的对应session
                COMMON.MemecacheHelper.Delete(key);
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect("/Login/Index");
        }

        public ActionResult CheckLogin()
        {
            //1 从session读取验证码
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();

            //2 验证码校验
            //2.1 验证码若为空则说明session中没有验证码
            if(string.IsNullOrEmpty(validateCode))
            {
                return Content("no:服务器错误");
            }
            //只要验证码不为空，则先清除session中的验证码
            Session["validateCode"] = null;
            //2.2 获取前台页面提交请求中包含验证码的Name属性
            string requestCode = Request["vCode"];
            //2.2.1 判断服务器session中保存的验证码与服务器发送的验证码是否相同
            if(!requestCode.Equals(validateCode,StringComparison.InvariantCultureIgnoreCase))   //忽略大小写
            {
                return Content("no:验证码错误");
            }
            else
            {
                //2.2.2 拿到浏览器传过来的用户名与密码
                string userName = Request["LoginCode"];
                string userPwd = Request["LoginPwd"];
                UserInfo userInfo = userInfoBLL.GetList(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();
                //var testInfo = new { id = 1, name = "测试" };
                //COMMON.SerializerHelper.SerializerToString(testInfo);
                //2.2.3 判断若取出了对应的用户名与密码
                if (userInfo!=null)
                {
                    #region 由 Model2Memecache 替代以下代码
                    ////(1)生成session ID
                    //string sessionId = Guid.NewGuid().ToString();//创建Session的id，作为memcache的key
                    ////(2)将session id与userinfo对象存入memcache中
                    ////注意需要将userinfo序列化
                    //COMMON.MemecacheHelper.Set(sessionId,COMMON.SerializerHelper.SerializerToString(userInfo),DateTime.Now.AddMinutes(60));
                    ////(3)将创建的sessionId以cookie的形式返回给浏览器
                    //Response.Cookies["sessionId"].Value = sessionId;
                    #endregion
                    //已将保存userInfo的保存至Session中，并将对应的sessionId返回给浏览器保存
                    Model2Memecache(userInfo);

                    //（4）判断是否选中记住我
                    //若未选中则为null，若选中则为1
                    if (!string.IsNullOrEmpty(Request["checkMe"]))
                    {
                        //选中则将用户名与密码存入cookie中
                        HttpCookie cookie1 = new HttpCookie("cp1", userInfo.UName);
                        //使用md5的方式将查询到的用户密码加密
                        HttpCookie cookie2 = new HttpCookie("cp2", COMMON.WebCommon.MD5String(userInfo.UPwd));
                        //cookie设置失效时间
                        cookie1.Expires = DateTime.Now.AddHours(3);
                        cookie2.Expires = DateTime.Now.AddHours(3);
                        //将cookie添加至响应 的cookie集合中
                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                    }
                    return Content("ok:登录成功");

                }
                else
                {

                    return Content("no:用户名或密码错误");
                }
            }
        }

        /// <summary>
        /// 将userInfo实体对象写入Memecache中保存
        /// 并将对应的sessionId写入cookie中并通过响应返回
        /// </summary>
        /// <param name="model">需要写入Session的UserInfo实体对象</param>
        protected void Model2Memecache(UserInfo model)
        {
            //(1)生成session ID
            string sessionId = Guid.NewGuid().ToString();//创建Session的id，作为memcache的key
            //(2)将session id与userinfo对象存入memcache中
            //注意需要将userinfo序列化
            COMMON.MemecacheHelper.Set(sessionId, COMMON.SerializerHelper.SerializerToString(model), DateTime.Now.AddMinutes(60));
            //(3)将创建的sessionId以cookie的形式返回给浏览器
            Response.Cookies["sessionId"].Value = sessionId;
        }

        /// <summary>
        /// 判断cookie信息
        /// </summary>
        public void CheckCookieInfo()
        {
            //判断cookie中是否包含 用户名（cp1）和密码（cp2）
            if (Request.Cookies["cp1"] != null&&Request.Cookies["cp2"]!=null)
            {
                //1 取出cookie中cp1，cp2 对应的值
                string userName = Request.Cookies["cp1"].Value;
                string userPwd = Request.Cookies["cp2"].Value;
                //2 取出对应用户名的用户对象
                var userInfo = userInfoBLL.GetList(u => u.UName == userName).FirstOrDefault();
                if(userInfo!=null)
                {
                    //2.1 判断cookie中保存的加密后的密码与当前密码是否相同
                    if(COMMON.WebCommon.MD5String(userInfo.UPwd)==userPwd)
                    {
                        #region 由 Model2Memecache 替代以下代码
                        ////(1)生成session ID
                        //string sessionId = Guid.NewGuid().ToString();//创建Session的id，作为memcache的key
                        ////(2)将session id与userinfo对象存入memcache中
                        ////注意需要将userinfo序列化
                        //COMMON.MemecacheHelper.Set(sessionId, COMMON.SerializerHelper.SerializerToString(userInfo), DateTime.Now.AddMinutes(60));
                        ////(3)将创建的sessionId以cookie的形式返回给浏览器
                        //Response.Cookies["sessionId"].Value = sessionId;
                        #endregion
                        //2.2 将userInfo实体对象写入Memecache中保存，并将对应的sessionId写入cookie中并通过响应返回
                        Model2Memecache(userInfo);
                        //2.3 向响应中加入重定向url
                        Response.Redirect("/MyHome/Index");

                    }
                }
                //3 清除当前请求中的cookie
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}