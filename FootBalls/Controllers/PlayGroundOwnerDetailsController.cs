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
    public class PlayGroundOwnerDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();
        private readonly object PGOwnerReferenceNumber;

        // GET: PlayGroundOwnerDetails
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult PlayGroundOwner(int? page)
        {

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var pgownerid = db.PlayGroundOwner_tbl.Where(x => x.UserId == userId).Select(x => x.PGOwnerId).FirstOrDefault();
                if (pgownerid != 0)
                {
                    Session["PGOwnerId"] = pgownerid;
                }
            }

            List<TblPlayGroundOwner> pgownerinfo = db.PlayGroundOwner_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (pgownerinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(pgownerinfo.ToPagedList(pageNumber, pageSize));
            } 

            return View();
        }

        [HttpGet]
        public ActionResult PlayGroundOwnerRegistration()
        {
            ViewBag.Name = Session["Name"].ToString();
            ViewBag.MobileNumber = Session["MobileNumber"].ToString();

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            return View();
           
        }

        [HttpPost]
        public ActionResult PlayGroundOwnerRegistration(TblPlayGroundOwner model, string city, string Category,HttpPostedFileBase postedFile)
        {
            var userid = Session["UserId"].ToString();

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            if (ModelState.IsValid)
            {
                
                db.PlayGroundOwner_tbl.Add(new TblPlayGroundOwner
                {
                    PGOwnerReferenceNumber = "1",
                    //PlayerId = model.PlayerId,
                    Name = model.Name,
                    Mobile=model.Mobile,
                    Category=model.Category,
                    CityId = Convert.ToInt32(city),
                    Confirmed = 1,
                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now,
                    UserId = Convert.ToInt32(userid),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("PlayGroundOwner");
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
        public ActionResult PlayGroundOwnerView(int id)
        {
            if (id != 0)
            {

                return View(db.PlayGroundOwner_tbl.Where(x => x.PGOwnerId == id && x.Status == 1).FirstOrDefault());
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
                return View(db.PlayGroundOwner_tbl.Where(x => x.PGOwnerId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblPlayGroundOwner model, string city)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            var EditPlayGroundOwnerList = db.PlayGroundOwner_tbl.Where(x => x.PGOwnerId == id && x.Status == 1).FirstOrDefault();
            if (EditPlayGroundOwnerList != null)
            {
                EditPlayGroundOwnerList.Name = model.Name;
                EditPlayGroundOwnerList.Mobile = model.Mobile;
                EditPlayGroundOwnerList.Category = model.Category;
                db.SaveChanges();
            }
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();           

            return Content("<script>alert('Updated Successfully');location.href='PlayGroundOwnerView';</script>");
        }
    }
}