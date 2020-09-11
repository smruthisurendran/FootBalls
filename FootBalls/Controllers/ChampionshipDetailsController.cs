using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using FootBalls.Models;
using PagedList;
using System.Web.Routing;

namespace FootBalls.Controllers
{
    public class ChampionshipDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();
        public static byte[] bytes;


        // GET: ChampionshipDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Championship(int? page)
        {

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var championshipsponsorid = db.ChampionshipSponsor_tbl.Where(x => x.UserId == userId).Select(x => x.ChampionshipSponsorId).FirstOrDefault();
                if (championshipsponsorid != 0)
                {
                    Session["ChampionshipSponsorId"] = championshipsponsorid;
                }
            }

           List<TblChampionship> champinfo = db.Championship_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (champinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(champinfo.ToPagedList(pageNumber, pageSize));
            }    

            return View();
        }

        [HttpGet]
        public ActionResult ChampionshipRegistration()
        {
           
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            return View();
          
        }

        [HttpPost]
        public ActionResult ChampionshipRegistration(TblChampionship model,string Category, string city, HttpPostedFileBase postedFile)
        {
            var userid = Session["UserId"].ToString();
            var championshipsponsorid = Session["ChampionshipSponsorId"].ToString();

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
                
                db.Championship_tbl.Add(new TblChampionship
                {
                    ChampionshipReferenceNumber = "1",
                    Championship = model.Championship,
                    Category = model.Category,
                    ChampionshipStartDate = model.ChampionshipStartDate,
                    ChampionshipEndDate = model.ChampionshipEndDate,
                    CityId = Convert.ToInt32(city),
                    Image = bytes,
                    SponsorId = 1,
                    ChampionshipSponsorId = Convert.ToInt32(championshipsponsorid),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });

                db.SaveChanges();

                return RedirectToAction("Championship");
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
        public ActionResult ChampionshipView(int id)
        {
            if (id != 0)
            {
                var img = db.Championship_tbl.Where(x => x.ChampionshipId == id && x.Status == 1).Select(x => x.Image).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }
                return View(db.Championship_tbl.Where(x => x.ChampionshipId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }


        public ActionResult EditProfile(int id)
        {
            List<TblCountry> countries = db.Country_tbl.ToList();
            ViewBag.CountryList = new SelectList(countries, "CountryId", "Country");

            if (id != 0)
            {
                return View(db.Championship_tbl.Where(x => x.ChampionshipId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblChampionship model, string city, HttpPostedFileBase postedFile)
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
            var EditChampionshipList = db.Championship_tbl.Where(x => x.ChampionshipId == id && x.Status == 1).FirstOrDefault();
            if (EditChampionshipList != null)
            {
                EditChampionshipList.Championship = model.Championship;
                EditChampionshipList.Category = model.Category;
                EditChampionshipList.ChampionshipStartDate = model.ChampionshipStartDate;
                EditChampionshipList.ChampionshipEndDate = model.ChampionshipEndDate;

                if (bytes != null)
                {
                    EditChampionshipList.Image = bytes;
                }
                db.SaveChanges();
            }
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();           

            return Content("<script>alert('Updated Successfully');location.href='ChampionshipView';</script>");
        }



        [HttpGet]
        public ActionResult Championships(int? page,int id)
        {
            if (id != 0)
            {
                Session["PGOwnerId"] = id;
            }

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var championshipsponsorid = db.ChampionshipSponsor_tbl.Where(x => x.UserId == userId).Select(x => x.ChampionshipSponsorId).FirstOrDefault();
                if (championshipsponsorid != 0)
                {
                    Session["ChampionshipSponsorId"] = championshipsponsorid;
                }
            }

            List<TblChampionship> champinfo = db.Championship_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (champinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(champinfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }


        [HttpGet]
        public ActionResult ChampionshipRequest(int id)
        {

            if (id != 0 && Session["PGOwnerId"] != null)
            {
                var pgOwnerId = Session["PGOwnerId"].ToString();
                int PGOwnerId = Convert.ToInt32(pgOwnerId);
                int ChampionshipId = id;

                db.ChampionshipRequest_tbl.Add(new TblChampionshipRequest
                {

                    ChampionshipId = ChampionshipId,
                    EntityId = PGOwnerId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14),
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 1,
                    ModifiedDate = DateTime.Now


                });
                db.SaveChanges();

                return RedirectToAction("Championships", "ChampionshipDetails", new RouteValueDictionary(new { id = PGOwnerId }));
            }
            return View();
        }


        [HttpGet]
        public ActionResult RequestForChampionship(int? page, int id)
        {
            if (id != 0)
            {
                Session["ChampionshipId"] = id;
                List<TblChampionshipRequest> championshipinfo = db.ChampionshipRequest_tbl.Where(x => x.ChampionshipId == id && x.Status == 1).ToList();
                if (championshipinfo != null)
                {
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    ViewBag.ChampionshipId = id;
                    return View(championshipinfo.ToPagedList(pageNumber, pageSize));
                }
            }
            return View();
        }



        [HttpGet]
        public ActionResult RequestStatusUpdate(int id, int crid)
        {
            int ChampionshipId = Convert.ToInt32(Session["ChampionshipId"].ToString());
            var result = db.ChampionshipRequest_tbl.SingleOrDefault(x => x.ChampionshipRequestId== crid);
            if (result != null)
            {

                if (id == 1)
                {
                    result.Approved = id;
                    result.Status = 0;
                    db.SaveChanges();
                }
                else
                {
                    result.Approved = id;
                    result.Status = 0;
                    db.SaveChanges();
                }

            }

            return RedirectToAction("RequestForChampionship", "ChampionshipDetails", new RouteValueDictionary(new { id = ChampionshipId }));
        }

    }
}