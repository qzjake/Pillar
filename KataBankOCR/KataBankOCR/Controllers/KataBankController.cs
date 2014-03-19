#region

using System.Web;
using System.Web.Mvc;
using KataBankOCR.Business;

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

            if (file == null || file.ContentLength <= 0) return RedirectToAction("Index");

            var ocr = new KataBankOcr(file.InputStream);

            ViewBag.Results = ocr.ProcessFileWithChecksum();

            return View("Results");
        }
    }
}