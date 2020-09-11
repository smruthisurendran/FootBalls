using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace FootBalls.Controllers
{
    public class PhotoController : Controller
    {
        

        // GET: Photo
        public ActionResult Index()
        {
            Session["val"] = " ";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Imagename)
        {
            ViewBag.pic = "http://localhost:7865/WebImages/" + Session["val"].ToString();
            System.IO.File.WriteAllText(Server.MapPath("~/WebImages/" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt"), Imagename);
            return View();
        }

        [HttpGet]
        public ActionResult Changephoto()
        {
            if (Convert.ToString(Session["val"]) != string.Empty)
            {
                ViewBag.pic = "http://localhost:7865/WebImages/" + Session["val"].ToString();
                
                
            }
            else
            {
                ViewBag.pic = "http://localhost:7865/WebImages/person.png";
            }
            return View();
        }

        public JsonResult Rebind()
        {
            string path = "http://localhost:7865/WebImages/" + Session["val"].ToString();
            return Json(path, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Capture()
        {
            var stream = Request.InputStream;
            string dump;
            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();
                DateTime nm = DateTime.Now;
                string date = nm.ToString("yyyymmddMMss");
                var path = Server.MapPath("~/WebImages/{0}.png" + date + "test.png");
               
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
                ViewData["path"] = date + "test.png";
                Session["val"] = date + "test.png";
            }
            return View("Index");
        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
    }
}