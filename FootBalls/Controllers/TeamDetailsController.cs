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
    public class TeamDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();
        public object TeamReferenceNumber;

        // GET: TeamDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Team(int? page)
        {

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var teamsponsorid = db.TeamSponsor_tbl.Where(x => x.UserId== userId).Select(x => x.TeamSponsorId).FirstOrDefault();
                if (teamsponsorid != 0)
                {
                    Session["TeamSponsorId"] = teamsponsorid;
                }
            }

           List<TblTeam> teaminfo = db.Team_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (teaminfo != null)
            {
               int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(teaminfo.ToPagedList(pageNumber, pageSize));
            } 
            
            return View();
        }

        [HttpGet]
        public ActionResult TeamRegistration()
        {
            //if (Session["UserId"] != null)
            //{

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            return View();
            //}


            //return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult TeamRegistration(TblTeam model, string city,  HttpPostedFileBase postedFile)
        {
            var userid = Session["UserId"].ToString();
            var teamsponsorid = Session["TeamSponsorId"].ToString();

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
                //TblPlayer tblPlayer = new TblPlayer();



                db.Team_tbl.Add(new TblTeam
                {
                    TeamReferenceNumber = "1",
                    //PlayerId = model.PlayerId,
                    TeamName = model.TeamName,
                  
                    CityId = Convert.ToInt32(city),
                   

                    //if(file!=null)
                    //{ 
                    //  string path = Path.Combine(Server.MapPath("~/UserImages"), Path.GetFileName(file.FileName));
                    //file.SaveAs(path);
                    //}


                    Photo = bytes,
                    Confirmed = 1,

                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now,

                   
                    TeamSponsorId = Convert.ToInt32(teamsponsorid),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("Team");
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
        public ActionResult TeamView(int id)
        {
            if (id != 0)
            {
                var img = db.Team_tbl.Where(x => x.TeamId == id && x.Status == 1).Select(x => x.Photo).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }
                return View(db.Team_tbl.Where(x => x.TeamId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }
    
    }
}