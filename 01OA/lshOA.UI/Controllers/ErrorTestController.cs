using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Controllers
{
    public class ErrorTestController : Controller
    {
        // GET: ErrorTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorTest()
        {
            return View();
            //int a = 2;
            //int b = 0;
            //int c = a / b;
            //return Content(c.ToString());
        }
    }
}