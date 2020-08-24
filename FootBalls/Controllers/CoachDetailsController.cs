using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FootBalls.Models;
using System.Data;
using PagedList;
using System.Web.Routing;

namespace FootBalls.Controllers
{
    public class CoachDetailsController : Controller
    {
        public static byte[] bytes;
        AllUsersContext db = new AllUsersContext();


        // GET: CoachDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Coach(int? page)
        {
            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var coachid = db.Coach_tbl.Where(x => x.UserId == userId).Select(x => x.CoachId).FirstOrDefault();
                if (coachid != 0)
                {
                    Session["CoachId"] = coachid;
                }
            }

            List<TblCoach> coachinfo = db.Coach_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (coachinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(coachinfo.ToPagedList(pageNumber, pageSize));
            }

            return View();

        }

        [HttpGet]
        public ActionResult CoachRegistration()
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
        public ActionResult CoachRegistration(TblCoach model, string city, HttpPostedFileBase postedFile)
        {
            var userid = Session["UserId"].ToString();

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



                db.Coach_tbl.Add(new TblCoach
                {
                    CoachReferenceNumber = "1",
                    //PlayerId = model.PlayerId,
                    Coach = model.Coach,
                    DOB = model.DOB,
                    CityId = Convert.ToInt32(city),
                    Length = model.Length,
                    Weight = model.Weight,


                    //if(file!=null)
                    //{ 
                    //  string path = Path.Combine(Server.MapPath("~/UserImages"), Path.GetFileName(file.FileName));
                    //file.SaveAs(path);
                    //}


                    Photo = bytes,
                    Confirmed = 1,

                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now,

                    SponsorId = 1,
                    Mobile = model.Mobile,
                    UserId = Convert.ToInt32(userid),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("Coach");
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
        public ActionResult CoachView(int id)
        {
            if (id != 0)
            {
                var teamMemberResult = db.TeamMembers_tbl.Where(x => x.CoachId == id).ToList();
                var img = db.Coach_tbl.Where(x => x.CoachId == id && x.Status == 1).Select(x => x.Photo).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }
                var coachResult = db.Coach_tbl.Where(x => x.CoachId == id && x.Status == 1).FirstOrDefault();
                var model = new CoachDetailsTbl { TeamMembersTbl = teamMemberResult, CoachTbl = coachResult };
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Coachh(int? page, int id)
        {
            if (id != 0)
            {
                Session["TeamId"] = id;
            }

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var coachid = db.Coach_tbl.Where(x => x.UserId == userId).Select(x => x.CoachId).FirstOrDefault();
                if (coachid != 0)
                {
                    Session["CoachId"] = coachid;
                }
            }


            List<TblCoach> coachinfo = db.Coach_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (coachinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(coachinfo.ToPagedList(pageNumber, pageSize));
            }

            return View();

        }

        [HttpGet]
        public ActionResult CoachRequest(int id)
        {

            if (id != 0 && Session["TeamId"] != null)
            {
                var teamId = Session["TeamId"].ToString();
                int TeamId = Convert.ToInt32(teamId);
                int CoachId = id;

                var TeamMemberId = db.TeamMembers_tbl.Where(x => x.TeamId == TeamId && x.CoachId == CoachId && x.Status == 1).Select(x => x.TeamMemberId).ToList();
                var CoachRequestId = db.CoachRequest_tbl.Where(x => x.RequestFrom == TeamId && x.CoachId == CoachId && x.Status == 1).Select(x => x.CoachRequestId).ToList();

                if (CoachRequestId != null && CoachRequestId.Count != 0)
                {

                    TempData["Alert"] = "Sorry. Request Already Send.";
                    return RedirectToAction("Coachh", "CoachDetails", new RouteValueDictionary(new { id = TeamId }));
                }

                else if (TeamMemberId != null && TeamMemberId.Count != 0)
                {
                    TempData["Alert"] = "Sorry. This Coach is Already In Your Team.";
                    return RedirectToAction("Coachh", "CoachDetails", new RouteValueDictionary(new { id = TeamId }));
                }

                else
                {
                    db.CoachRequest_tbl.Add(new TblCoachRequest
                    {
                        CoachId = CoachId,
                        RequestFrom = TeamId,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(14),
                        Status = 1,
                        CreatedId = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedId = 1,
                        ModifiedDate = DateTime.Now

                    });
                    db.SaveChanges();
                    TempData["Alert"] = "Request Send Successfully.";
                    return RedirectToAction("Coachh", "CoachDetails", new RouteValueDictionary(new { id = TeamId }));
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RequestForCoach(int? page, int id)
        {
            if (id != 0)
            {
                Session["CoachId"] = id;
                List<TblCoachRequest> coachinfo = db.CoachRequest_tbl.Where(x => x.CoachId == id && x.Status == 1).ToList();
                if (coachinfo != null)
                {
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    ViewBag.CoachId = id;
                    return View(coachinfo.ToPagedList(pageNumber, pageSize));
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RequestStatusUpdate(int id, int prid)
        {
            int Coachid = Convert.ToInt32(Session["CoachId"].ToString());
            var result = db.CoachRequest_tbl.SingleOrDefault(x => x.CoachRequestId == prid);
            if (result != null)
            {

                if (id == 1)
                {
                    result.Approved = id;
                    result.Status = 0;
                    db.SaveChanges();
                    db.TeamMembers_tbl.Add(new TblTeamMembers
                    {
                        TeamId = result.RequestFrom,
                        TeamReferenceNumber = "1",
                        Status = 1,
                        CreatedId = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedId = 1,
                        ModifiedDate = DateTime.Now,
                        CoachId = Coachid

                    });
                    db.SaveChanges();
                }
                else
                {
                    result.Approved = id;
                    result.Status = 0;
                    db.SaveChanges();
                }

            }

            return RedirectToAction("RequestForCoach", "CoachDetails", new RouteValueDictionary(new { id = Coachid }));
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            if (id != 0)
            {
                return View(db.Coach_tbl.Where(x => x.CoachId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblCoach model, string city, HttpPostedFileBase postedFile)
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
            var EditCoachList = db.Coach_tbl.Where(x => x.CoachId == id && x.Status == 1).FirstOrDefault();
            if (EditCoachList != null)
            {
                EditCoachList.Coach = model.Coach;
                EditCoachList.DOB = model.DOB;
                EditCoachList.Length = model.Length;
                EditCoachList.Weight = model.Weight;
                EditCoachList.Mobile = model.Mobile;
                if (bytes != null)
                {
                    EditCoachList.Photo = bytes;
                }
                db.SaveChanges();
            }
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();           

            return Content("<script>alert('Updated Successfully');location.href='';</script>");
        }

        [HttpGet]
        public ActionResult MyRequest(int id)
        {
            ViewBag.Coach = id;
            var coachRequest = db.CoachRequest_tbl.Where(x => x.CoachId == id).ToList();
            var teamRequest = db.TeamRequest_tbl.Where(x => x.RequestFrom == id && x.MemberConfirmId == 2).ToList();
            var model = new CoachMyRequestTbl { CoachRequestTbl = coachRequest, TeamRequestTbl = teamRequest };
            return View(model);
        }
    }
}
