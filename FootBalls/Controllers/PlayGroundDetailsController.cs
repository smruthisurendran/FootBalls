using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FootBalls.Models;
using System.Data;
using PagedList;


namespace FootBalls.Controllers
{
    public class PlayGroundDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();
        public static byte[] bytes;

        // GET: PlayGroundDetaills
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PlayGround(int? page)
        {

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var pgownerid = db.PlayGroundOwner_tbl.Where(x => x.UserId == userId).Select(x => x.PGOwnerId).FirstOrDefault();
                if (pgownerid != 0)
                {
                    Session["PGId"] = pgownerid;
                }
            }

            List<TblPlayGround> pginfo = db.PlayGround_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (pginfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(pginfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }

        [HttpGet]
        public ActionResult PlayGroundRegistration()
        {
            
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            return View();
          
        }

        [HttpPost]
        public ActionResult PlayGroundRegistration(TblPlayGround model, string city, HttpPostedFileBase postedFile)
        {
            var userid = Session["UserId"].ToString();
            var pgownerid = Session["PGOwnerId"].ToString();
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            if (ModelState.IsValid)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                db.PlayGround_tbl.Add(new TblPlayGround
                {
                    PGReferenceNumber = "1",
                    PGOwnerId = Convert.ToInt32(pgownerid),
                    Name = model.Name,
                    Length = model.Length,
                    Width = model.Width,
                    GoalLength = model.GoalLength,
                    GoalWidth = model.GoalWidth,
                    NoOfPlayer = model.NoOfPlayer,
                    CityId = Convert.ToInt32(city),
                    Location = model.Location,
                    Image = bytes,
                    RentingPrice = model.RentingPrice,
                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddMonths(12),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("PlayGround");
            }
            return View();

        }

        // dropdown for region based on nation 
        public JsonResult getRegion(int id)
        {
            List<TblRegion> regions = db.Region_tbl.Where(x => x.CountryId == id).ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");


            return Json(new SelectList(regions, "RegionId", "Region", JsonRequestBehavior.AllowGet));
        }

        // dropdown for location based on region
        public JsonResult getCity(int id)
        {
            List<TblCity> cities = db.City_tbl.Where(x => x.Regionid == id).ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");


            return Json(new SelectList(cities, "CityId", "City", JsonRequestBehavior.AllowGet));
        }


        [HttpGet]
        public ActionResult PlayGroundView(int id)
        {
            if (id != 0)
            {
                var img = db.PlayGround_tbl.Where(x => x.PGId == id && x.Status == 1).Select(x => x.Image).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }
                return View(db.PlayGround_tbl.Where(x => x.PGId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            if (id != 0)
            {
                return View(db.PlayGround_tbl.Where(x => x.PGId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblPlayGround model, string city, HttpPostedFileBase postedFile)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            if (postedFile != null)
            {
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
            }
            var EditPlayGroundList = db.PlayGround_tbl.Where(x => x.PGId == id && x.Status == 1).FirstOrDefault();
            if (EditPlayGroundList != null)
            {
                EditPlayGroundList.Name = model.Name;
                EditPlayGroundList.Length = model.Length;
                EditPlayGroundList.Width = model.Width;
                EditPlayGroundList.GoalLength = model.GoalLength;
                EditPlayGroundList.GoalWidth = model.GoalWidth;
                EditPlayGroundList.NoOfPlayer = model.NoOfPlayer;
                EditPlayGroundList.Location = model.Location;
                EditPlayGroundList.RentingPrice = model.RentingPrice;
                if (bytes != null)
                {
                    EditPlayGroundList.Image = bytes;
                }
                db.SaveChanges();
            }           
            return Content("<script>alert('Updated Successfully');location.href='PlayGroundView';</script>");
        }

    }
}