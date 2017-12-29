using ImgRecognition.BusinessLogic.DataServices.Implementation;
using System.IO;
using System.Web.Mvc;

namespace ImgRecognition.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var fileName = "C:\\Work\\_personal\\ImgRecognition\\ImgRecognition.Web\\Samples\\coala.jpg";

            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            Stream stream = new MemoryStream(bytes);

            var imgRecognition = new CognitiveImgRecognition();
            var result = imgRecognition.RecognizeImage(stream);

            string s = result.Result.Description;

            return View();
        }
    }
}
