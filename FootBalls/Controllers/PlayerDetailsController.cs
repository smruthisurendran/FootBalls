﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FootBalls.Models;
using System.Data;
using PagedList;
using System.Web.Routing;
using System.Drawing;

namespace FootBalls.Controllers
{
    public class PlayerDetailsController : Controller
    {
        public static byte[] bytes;
        public static List<TblPlayer> searchResult = null;
        AllUsersContext db = new AllUsersContext();



        // GET: PlayerDetails
        public ActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public ActionResult Player(int? page)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

            List<TblPlayingPlace> playingPlace = db.PlayingPlace_tbl.ToList();
            ViewBag.PlayingPlaceList = new SelectList(playingPlace, "PlayingPlaceId", "PlayingPlace");

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var playerid = db.Player_tbl.Where(x => x.UserId == userId).Select(x => x.PlayerId).FirstOrDefault();
                if (playerid != 0)
                {
                    Session["PlayerId"] = playerid;
                }
            }

            List<TblPlayer> playerinfo = db.Player_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (playerinfo != null)
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(playerinfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }


        [HttpPost]
        public ActionResult Player(int? page, string name, string weight, string height, string playingplace, string playingfoot, string city, string country, string region)
        {

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

            List<TblPlayingPlace> playingPlace = db.PlayingPlace_tbl.ToList();
            ViewBag.PlayingPlaceList = new SelectList(playingPlace, "PlayingPlaceId", "PlayingPlace");

            if (name != null && name != "")
            {
                searchResult = db.Player_tbl.Where(x => x.Player.ToLower() == name.ToLower()).ToList();
            }
            else
            {
                searchResult = db.Player_tbl.ToList();
            }
            if (weight != null && weight != "")
            {
                int Weight = Convert.ToInt32(weight);
                searchResult = searchResult.Where(x => x.Weight == Weight).ToList();
            }
            if (height != null && height != "")
            {
                int Height = Convert.ToInt32(height);
                searchResult = searchResult.Where(x => x.Length == Height).ToList();
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
            if (playingplace != null && playingplace != "")
            {
                int Playingplace = Convert.ToInt32(playingplace);
                searchResult = searchResult.Where(x => x.PlayingPlace == Playingplace).ToList();
            }
            if (playingfoot != null && playingfoot != "")
            {
                int Playingfoot = Convert.ToInt32(playingfoot);
                searchResult = searchResult.Where(x => x.PlayingFoot == Playingfoot).ToList();
            }

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var playerid = db.Player_tbl.Where(x => x.UserId == userId).Select(x => x.PlayerId).FirstOrDefault();
                if (playerid != 0)
                {
                    Session["PlayerId"] = playerid;
                }
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(searchResult.ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        public ActionResult PlayerRegistration()
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
        public ActionResult PlayerRegistration(TblPlayer model, string city,List<string> PlayingPlace, HttpPostedFileBase postedFile)
        {
            var userid = Session["UserId"].ToString();

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            //if (PlayingPlace != null)
            //{
               
            //    string selectedvalue = string.Join(",", PlayingPlace);
                
            //}


            if (ModelState.IsValid)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                //TblPlayer tblPlayer = new TblPlayer();



                db.Player_tbl.Add(new TblPlayer
                {
                    PlayerReferenceNumber = "1",
                    //PlayerId = model.PlayerId,
                    Player = model.Player,
                    DOB = model.DOB,
                    CityId = Convert.ToInt32(city),
                    Length = model.Length,
                    Weight = model.Weight,
                    PlayingFoot = model.PlayingFoot,
                    PlayingPlace = model.PlayingPlace,

                    //if(file!=null)
                    //{ 
                    //  string path = Path.Combine(Server.MapPath("~/UserImages"), Path.GetFileName(file.FileName));
                    //file.SaveAs(path);
                    //}


                    Photo = bytes,
                    Confirmed = 1,

                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddYears(1),

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



                return RedirectToAction("Player");
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
        public ActionResult PlayerView(int id)
        {
            if (id != 0)
            {
                var teamMemberResult = db.TeamMembers_tbl.Where(x => x.PlayerId == id).ToList();
                var img = db.Player_tbl.Where(x => x.PlayerId == id && x.Status == 1).Select(x => x.Photo).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }
                var playerResult = db.Player_tbl.Where(x => x.PlayerId == id && x.Status == 1).FirstOrDefault();
                var model = new PlayerDetailsTbl { TeamMembersTbl = teamMemberResult, PlayerTbl = playerResult };
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult PlayerS(int? page, int id)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

            List<TblPlayingPlace> playingPlace = db.PlayingPlace_tbl.ToList();
            ViewBag.PlayingPlaceList = new SelectList(playingPlace, "PlayingPlaceId", "PlayingPlace");

            if (id != 0)
            {
                Session["TeamId"] = id;
            }

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var playerid = db.Player_tbl.Where(x => x.UserId == userId).Select(x => x.PlayerId).FirstOrDefault();
                if (playerid != 0)
                {
                    Session["PlayerId"] = playerid;
                }
            }

            List<TblPlayer> playerinfo = db.Player_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (playerinfo != null)
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(playerinfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }
        [HttpPost]
        public ActionResult PlayerS(int? page, string name, string weight, string height, string playingplace, string playingfoot, string city, string country, string region)
        {

            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblRegion> regions = db.Region_tbl.ToList();
            ViewBag.RegionList = new SelectList(regions, "RegionId", "Region");

            List<TblCity> cities = db.City_tbl.ToList();
            ViewBag.CityList = new SelectList(cities, "CityId", "City");

            List<TblPlayingPlace> playingPlace = db.PlayingPlace_tbl.ToList();
            ViewBag.PlayingPlaceList = new SelectList(playingPlace, "PlayingPlaceId", "PlayingPlace");

            if (name != null && name != "")
            {
                searchResult = db.Player_tbl.Where(x => x.Player.ToLower() == name.ToLower()).ToList();
            }
            else
            {
                searchResult = db.Player_tbl.ToList();
            }
            if (weight != null && weight != "")
            {
                int Weight = Convert.ToInt32(weight);
                searchResult = searchResult.Where(x => x.Weight == Weight).ToList();
            }
            if (height != null && height != "")
            {
                int Height = Convert.ToInt32(height);
                searchResult = searchResult.Where(x => x.Length == Height).ToList();
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
            if (playingplace != null && playingplace != "")
            {
                int Playingplace = Convert.ToInt32(playingplace);
                searchResult = searchResult.Where(x => x.PlayingPlace == Playingplace).ToList();
            }
            if (playingfoot != null && playingfoot != "")
            {
                int Playingfoot = Convert.ToInt32(playingfoot);
                searchResult = searchResult.Where(x => x.PlayingFoot == Playingfoot).ToList();
            }

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var playerid = db.Player_tbl.Where(x => x.UserId == userId).Select(x => x.PlayerId).FirstOrDefault();
                if (playerid != 0)
                {
                    Session["PlayerId"] = playerid;
                }
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(searchResult.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult PlayerRequest(int id)
        {

            if (id != 0 && Session["TeamId"] != null)
            {
                var teamId = Session["TeamId"].ToString();
                int TeamId = Convert.ToInt32(teamId);
                int PlayerId = id;

                var TeamMemberId = db.TeamMembers_tbl.Where(x => x.TeamId == TeamId && x.PlayerId == PlayerId && x.Status == 1).Select(x => x.TeamMemberId).ToList();
                var PlayerRequestId = db.PlayerRequest_tbl.Where(x => x.RequestFrom == TeamId && x.PlayerId == PlayerId && x.Status == 1).Select(x => x.PlayerRequestId).ToList();

                if (PlayerRequestId != null && PlayerRequestId.Count != 0)
                {

                    TempData["Alert"] = "Sorry. Request Already Send.";
                    return RedirectToAction("PlayerS", "PlayerDetails", new RouteValueDictionary(new { id = TeamId }));
                }

                else if (TeamMemberId != null && TeamMemberId.Count != 0)
                {
                    TempData["Alert"] = "Sorry. This Player is Already In Your Team.";
                    return RedirectToAction("PlayerS", "PlayerDetails", new RouteValueDictionary(new { id = TeamId }));
                }

                else
                {
                    db.PlayerRequest_tbl.Add(new TblPlayerRequest
                    {
                        PlayerId = PlayerId,
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
                    return RedirectToAction("PlayerS", "PlayerDetails", new RouteValueDictionary(new { id = TeamId }));
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult RequestForPlayer(int? page, int id)
        {
            if (id != 0)
            {
                Session["PlayerId"] = id;
                List<TblPlayerRequest> palyerinfo = db.PlayerRequest_tbl.Where(x => x.PlayerId == id && x.Status == 1).ToList();
                if (palyerinfo != null)
                {
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    ViewBag.PlayerId = id;
                    return View(palyerinfo.ToPagedList(pageNumber, pageSize));
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RequestStatusUpdate(int id, int prid)
        {
            int Playerid = Convert.ToInt32(Session["PlayerId"].ToString());
            var result = db.PlayerRequest_tbl.SingleOrDefault(x => x.PlayerRequestId == prid);
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
                        PlayerId = Playerid

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

            return RedirectToAction("RequestForPlayer", "PlayerDetails", new RouteValueDictionary(new { id = Playerid }));
        }


        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            if (id != 0)
            {
                return View(db.Player_tbl.Where(x => x.PlayerId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblPlayer model, string city, HttpPostedFileBase postedFile)
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
            var EditPlayerList = db.Player_tbl.Where(x => x.PlayerId == id && x.Status == 1).FirstOrDefault();
            if (EditPlayerList != null)
            {
                EditPlayerList.Player = model.Player;
                EditPlayerList.DOB = model.DOB;
                EditPlayerList.Length = model.Length;
                EditPlayerList.Weight = model.Weight;
                EditPlayerList.PlayingFoot = model.PlayingFoot;
                EditPlayerList.Mobile = model.Mobile;
                if (bytes != null)
                {
                    EditPlayerList.Photo = bytes;
                }
                db.SaveChanges();
            }
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();           

            return Content("<script>alert('Updated Successfully');location.href='PlayerView';</script>");
        }

        [HttpGet]
        public ActionResult MyRequest(int id)
        {
            ViewBag.PlayerId = id;
            var playerRequest = db.PlayerRequest_tbl.Where(x => x.PlayerId == id).ToList();
            var teamRequest = db.TeamRequest_tbl.Where(x => x.RequestFrom == id && x.MemberConfirmId == 1).ToList();
            var model = new PlayerMyRequestTbl { PlayerRequestTbl = playerRequest, TeamRequestTbl = teamRequest };
            return View(model);
        }

        [HttpGet]
        public ActionResult PlayersAwards(int id)
        {
            ViewBag.PlayerId = id;
            return View();
        }

        [HttpGet]
        public ActionResult Championship(int id)
        {
            ViewBag.PlayerId = id;
            return View();
        }

        [HttpGet]
        public ActionResult Matches(int id)
        {
            ViewBag.PlayerId = id;
            return View();
        }

        [HttpGet]
        public ActionResult Teams(int id)
        {
            ViewBag.PlayerId = id;
            return View();
        }      
    }
}