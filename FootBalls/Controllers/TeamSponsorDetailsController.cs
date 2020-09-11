using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FootBalls.Models;
using System.Data;
using PagedList;

namespace FootBalls.Controllers
{
    public class TeamSponsorDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();

        // GET: TeamSponsorDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TeamSponsor(int? page)
        {

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var teamsponsorid = db.TeamSponsor_tbl.Where(x => x.UserId == userId).Select(x => x.TeamSponsorId).FirstOrDefault();
                if (teamsponsorid != 0)
                {
                    Session["TeamSponsorId"] = teamsponsorid;
                }
            }

            List<TblTeamSponsor> teamsponsorinfo = db.TeamSponsor_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (teamsponsorinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(teamsponsorinfo.ToPagedList(pageNumber, pageSize));
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult TeamSponsorRegistration()
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
        public ActionResult TeamSponsorRegistration(TblTeamSponsor model,string city, string Category)
        {
            var userid = Session["UserId"].ToString();

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            if (ModelState.IsValid)
            {

                db.TeamSponsor_tbl.Add(new TblTeamSponsor
                {
                    TeamSponsorReferenceNumber = "1",

                    Name = model.Name,
                    Category = model.Category,
                    Confirmed = 1,

                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddYears(1),

                    Mobile = model.Mobile,
                    UserId = Convert.ToInt32(userid),
                    CityId = Convert.ToInt32(city),
                    
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,

                    ModifiedId = 1,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("TeamSponsor");
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
        public ActionResult TeamSponsorView(int id)
        {
            if (id != 0)
            {

                return View(db.TeamSponsor_tbl.Where(x => x.TeamSponsorId == id).FirstOrDefault());
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
                return View(db.TeamSponsor_tbl.Where(x => x.TeamSponsorId == id).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblTeamSponsor model, string city)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            var EditTeamSponsorList = db.TeamSponsor_tbl.Where(x => x.TeamSponsorId == id).FirstOrDefault();
            if (EditTeamSponsorList != null)
            {
                EditTeamSponsorList.Name = model.Name;
                EditTeamSponsorList.Category = model.Category;
                EditTeamSponsorList.Mobile = model.Mobile;

                db.SaveChanges();
            }           
            return Content("<script>alert('Updated Successfully');location.href='TeamSponsorView';</script>");
        }
    }
}