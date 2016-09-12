using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HM13OA.IService;
using HM13OA.Model;
using HM13OA.Service;

namespace HM13OA.UI.Controllers
{
    public class UserInfoController : Controller
    {
        //UserInfoService UserInfoService=new UserInfoService();
        //IUserInfoService UserInfoService=new UserInfoService();

        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            //throw new Exception("nll");
            ViewData.Model=UserInfoService.GetList(u => true);
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(UserInfo userInfo)
        {
            string result = "no";

            //执行添加操作，并更改返回结果
            if (UserInfoService.Add(userInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetById(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            string result = "no";

            if (UserInfoService.Edit(userInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

    }
}
