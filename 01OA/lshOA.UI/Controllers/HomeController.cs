using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoBLL userInfoBLL = new BLL.UserInfoBLL();
        public ActionResult Index()
        {
            //
            var userInfo = userInfoBLL.GetList(u => u.DelFlag == 0);
            ViewData.Model = userInfo;
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}