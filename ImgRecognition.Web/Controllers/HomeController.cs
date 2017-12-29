using ImgRecognition.BusinessLogic.DataServices.Implementation;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ImgRecognition.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Samples"), _FileName);
                    file.SaveAs(_path);

                    byte[] bytes = System.IO.File.ReadAllBytes(_path);
                    Stream stream = new MemoryStream(bytes);

                    var imgRecognition = new CognitiveImgRecognition();
                    var result = imgRecognition.RecognizeImage(stream);

                    ViewBag.Img = _path;
                    ViewBag.Description = result.Result.Description;
                    ViewBag.Tags = result.Result.Tags;
                }

                ViewBag.Message = "File Uploaded Successfully!!";
                return View("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Index");
            }
        }

    }
}
