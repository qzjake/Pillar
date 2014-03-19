#region

using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

#endregion

namespace KataBankOCR.Controllers
{
    public class KataBankController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            ViewBag.File = file;
            if (file != null && file.ContentLength > 0)
            {                
                Debug.WriteLine("This will send the file to our Kata");
            }

            return RedirectToAction("Index");
        }
    }
}