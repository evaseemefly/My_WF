using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace lshOA.UI.Controllers
{
    public class AjaxUploadController : Controller
    {
        // GET: AjaxUpload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenuIcon()
        {
            string fileName = null;
            if (Request.Files.Count==0)
            {
                
            }
            else
            {
                HttpPostedFileBase file = Request.Files[0];
                fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName);
                //判断后缀是否为.png或者为.jpg
                if (fileExt == ".png" || fileExt == ".jpg")
                {
                    string newfileName = Guid.NewGuid().ToString() + fileExt;
                    file.SaveAs(Server.MapPath("/MenuIcon/" + newfileName));//将图片保存至新的路径
                    //return Content("ok:/MenuIcon/" + newfileName);
                }
                else
                {
                    //return Content("no");
                }
            }
            return Json(new {state="ok", imgName = fileName }, "text/html", JsonRequestBehavior.AllowGet);

        }
    }
}