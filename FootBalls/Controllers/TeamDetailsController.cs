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
    public class TeamDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();
        public object TeamReferenceNumber;
        public static List<int> TeamMemberId;
        public static byte[] bytes;
        public static List<TblTeam> searchResult = null;
        // GET: TeamDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Team(int? page)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

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

            List<TblTeam> teaminfo = db.Team_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (teaminfo != null)
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(teaminfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }

        [HttpPost]
        public ActionResult Team(int? page, string name, string city, string country, string region)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

            if (name != null && name != "")
            {
                searchResult = db.Team_tbl.Where(x => x.TeamName.ToLower() == name.ToLower()).ToList();
            }
            else
            {
                searchResult = db.Team_tbl.ToList();
            }           
            if (country != null && country != "")
            {
                searchResult = searchResult.Where(x => x.TblCity.TblRegion.TblCountry.Country.ToLower() == country.ToLower()).ToList();
            }
            if (region != null && region != "")
            {
                searchResult = searchResult.Where(x => x.TblCity.TblRegion.Region.ToLower() == region.ToLower()).ToList();
            }
            if (city != null && city != "")
            {
                searchResult = searchResult.Where(x => x.TblCity.City.ToLower() == city.ToLower()).ToList();
            }
      
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

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(searchResult.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult TeamRegistration()
        {
                 
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            return View();
            
        }

        [HttpPost]
        public ActionResult TeamRegistration(TblTeam model, string city, HttpPostedFileBase postedFile)
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
                var playerResult = db.PlayerRequest_tbl.Where(x =>x.RequestFrom == id && x.Approved == 1).ToList();
                var coachResult = db.CoachRequest_tbl.Where(x => x.RequestFrom == id && x.Approved == 1).ToList();
                //var teamMemberResult = db.TeamMembers_tbl.Where(x => x.TeamId == id).ToList();
                var img = db.Team_tbl.Where(x => x.TeamId == id && x.Status == 1).Select(x => x.Photo).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }
                var teamResult = db.Team_tbl.Where(x => x.TeamId == id && x.Status == 1).FirstOrDefault();
                //var model = from tmr in teamMemberResult
                //                        join pr in playerResult on tmr.PlayerId equals pr.PlayerId
                //                        join cr in coachResult on tmr.CoachId equals cr.CoachId
                var model = new TeamDetailsTbl {TeamTbl = teamResult,PlayerRequestTbl= playerResult, CoachRequestTbl= coachResult };
                return View(model);
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
                return View(db.Team_tbl.Where(x => x.TeamId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblTeam model, string city, HttpPostedFileBase postedFile)
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
            var EditTeamList = db.Team_tbl.Where(x => x.TeamId == id && x.Status == 1).FirstOrDefault();
            if (EditTeamList != null)
            {
                EditTeamList.TeamName = model.TeamName;

                if (bytes != null)
                {
                    EditTeamList.Photo = bytes;
                }
                db.SaveChanges();
            }
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();           

            return Content("<script>alert('Updated Successfully');location.href='TeamView';</script>");
        }

        [HttpGet]
        public ActionResult Teams(int? page, int id, int memberConfirmId)
        {
            if (id != 0 && memberConfirmId != 0)
            {
                Session["Id"] = id;
                Session["MemberConfirmId"] = memberConfirmId;
            }

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

            List<TblTeam> teaminfo = db.Team_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (teaminfo != null)
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(teaminfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }

        [HttpPost]
        public ActionResult Teams(int? page, string name, string city, string country, string region)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

            if (name != null && name != "")
            {
                searchResult = db.Team_tbl.Where(x => x.TeamName.ToLower() == name.ToLower()).ToList();
            }
            else
            {
                searchResult = db.Team_tbl.ToList();
            }
            if (country != null && country != "")
            {
                searchResult = searchResult.Where(x => x.TblCity.TblRegion.TblCountry.Country.ToLower() == country.ToLower()).ToList();
            }
            if (region != null && region != "")
            {
                searchResult = searchResult.Where(x => x.TblCity.TblRegion.Region.ToLower() == region.ToLower()).ToList();
            }
            if (city != null && city != "")
            {
                searchResult = searchResult.Where(x => x.TblCity.City.ToLower() == city.ToLower()).ToList();
            }

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

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(searchResult.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult TeamRequest(int id)
        {

            if (id != 0 && Session["Id"] != null && Session["MemberConfirmId"] != null)
            {

                int Id = Convert.ToInt32(Session["Id"].ToString());
                int MemberConfirmId = Convert.ToInt32(Session["MemberConfirmId"].ToString());
                int TeamId = id;

                if(MemberConfirmId == 1)
                {
                    TeamMemberId = db.TeamMembers_tbl.Where(x => x.TeamId == TeamId && x.PlayerId == Id && x.Status == 1).Select(x => x.TeamMemberId).ToList();
                }
                else
                {
                    TeamMemberId = db.TeamMembers_tbl.Where(x => x.TeamId == TeamId && x.CoachId == Id && x.Status == 1).Select(x => x.TeamMemberId).ToList();
                }
               
                var TeamRequestId = db.TeamRequest_tbl.Where(x => x.RequestFrom == Id && x.MemberConfirmId == MemberConfirmId && x.Status == 1).Select(x => x.TeamRequestId).ToList();

                if (TeamRequestId != null && TeamRequestId.Count != 0)
                {

                    TempData["Alert"] = "Sorry. Request Already Send.";
                    return RedirectToAction("Teams", "TeamDetails", new RouteValueDictionary(new { id = Id, memberConfirmId = MemberConfirmId }));
                }
                else if (TeamMemberId != null && TeamMemberId.Count != 0)
                {
                    TempData["Alert"] = "Sorry. You Already In this Team.";
                    return RedirectToAction("Teams", "TeamDetails", new RouteValueDictionary(new { id = Id, memberConfirmId = MemberConfirmId }));
                }
                else
                {
                db.TeamRequest_tbl.Add(new TblTeamRequest
                {
                    TeamId = TeamId,
                    RequestFrom = Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 1,
                    ModifiedDate = DateTime.Now,
                    MemberConfirmId = MemberConfirmId

                });
                db.SaveChanges();
                TempData["Alert"] = "Request Send Successfully.";
                return RedirectToAction("Teams", "TeamDetails", new RouteValueDictionary(new { id = Id, memberConfirmId = MemberConfirmId }));
                }
            }

            return View();
        }


        [HttpGet]
        public ActionResult RequestForTeam(int? page, int id)
        {

            if (id != 0)
            {
                Session["TeamId"] = id;
                List<TblTeamRequest> teaminfo = db.TeamRequest_tbl.Where(x => x.TeamId == id && x.Status == 1).ToList();
                if (teaminfo != null)
                {
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    ViewBag.TeamId = id;
                    return View(teaminfo.ToPagedList(pageNumber, pageSize));
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RequestStatusUpdate(int id, int trid, int memberConfirmId)
        {
            int TeamId = Convert.ToInt32(Session["TeamId"].ToString());
            var result = db.TeamRequest_tbl.SingleOrDefault(x => x.TeamRequestId == trid);
            if (result != null)
            {

                if (id == 1)
                {
                    result.Approved = id;
                    result.Status = 0;
                    db.SaveChanges();

                    TblTeamMembers tblTeamMembers = new TblTeamMembers();
                    tblTeamMembers.TeamId = TeamId;
                    tblTeamMembers.TeamReferenceNumber = "1";
                    tblTeamMembers.Status = 1;
                    tblTeamMembers.CreatedId = 1;
                    tblTeamMembers.CreatedDate = DateTime.Now;
                    tblTeamMembers.ModifiedId = 1;
                    tblTeamMembers.ModifiedDate = DateTime.Now;
                    if (memberConfirmId == 1)
                    {
                        tblTeamMembers.PlayerId = result.RequestFrom;
                    }
                    else
                    {
                        tblTeamMembers.CoachId = result.RequestFrom;
                    }
                    db.TeamMembers_tbl.Add(tblTeamMembers);
                    db.SaveChanges();
                }
                else
                {
                    result.Approved = id;
                    result.Status = 0;
                    db.SaveChanges();
                }

            }

            return RedirectToAction("RequestForTeam", "TeamDetails", new RouteValueDictionary(new { id = TeamId }));
        }
    }
}