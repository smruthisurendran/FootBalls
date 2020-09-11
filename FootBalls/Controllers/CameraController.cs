using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;


namespace FootBalls.Controllers
{
    public class CameraController : Controller
    {
        // GET: Camera
        public ActionResult Index()
        {
            string[] allimage = System.IO.Directory.GetFiles(Server.MapPath("~/CameraPhotos/"));
            if(allimage.Length>0)
            {
                List<string> base64text = new List<string>();
                foreach(var item in allimage)
                {
                    base64text.Add(System.IO.File.ReadAllText(item.ToString()));
                }
                ViewBag.Images = base64text;
            }
            return View();
        }

        [HttpPost]
        public void SaveImage(string base64image)
        {
            System.IO.File.WriteAllText(Server.MapPath("~/CameraPhotos/" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt"), base64image);
        }
    }
}