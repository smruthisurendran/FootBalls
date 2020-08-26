using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using FootBalls.Models;
using System.Data;
using PagedList;

namespace FootBalls.Controllers
{
    public class RefereeDetailsController : Controller
    {
        public static byte[] bytes;
        AllUsersContext db = new AllUsersContext();
        public object RefereeReferenceNumber;

        // GET: RefereeDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Referee(int? page)
        {

            if (Session["UserId"] != null)
            {
                var userid = Session["UserId"].ToString();
                int userId = Convert.ToInt32(userid);
                var refereeid = db.Referee_tbl.Where(x => x.UserId == userId).Select(x => x.RefereeId).FirstOrDefault();
                if (refereeid != 0)
                {
                    Session["RefereeId"] = refereeid;
                }
            }

            List<TblReferee> refereeinfo = db.Referee_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (refereeinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(refereeinfo.ToPagedList(pageNumber, pageSize));
            }

            return View();
        }

        [HttpGet]
        public ActionResult RefereeRegistration()
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
        public ActionResult RefereeRegistration(TblReferee model, string city, HttpPostedFileBase postedFile)
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



                db.Referee_tbl.Add(new TblReferee
                {
                    RefereeReferenceNumber = "1",
                    //PlayerId = model.PlayerId,
                    Referee = model.Referee,
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
                    RefereeCommision = model.RefereeCommision,
                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("Referee");
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
        public ActionResult RefereeView(int id)
        {
            if (id != 0)
            {
                var img = db.Referee_tbl.Where(x => x.RefereeId == id && x.Status == 1).Select(x => x.Photo).FirstOrDefault();
                if (img != null)
                {
                    string imreBase64Data = Convert.ToBase64String(img);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    ViewBag.ImageData = imgDataURL;
                }

                return View(db.Referee_tbl.Where(x => x.RefereeId == id && x.Status == 1).FirstOrDefault());
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
                return View(db.Referee_tbl.Where(x => x.RefereeId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(int id, TblReferee model, string city, HttpPostedFileBase postedFile)
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

            var EditRefereeList = db.Referee_tbl.Where(x => x.RefereeId == id && x.Status == 1).FirstOrDefault();
            if (EditRefereeList != null)
            {
                EditRefereeList.Referee = model.Referee;
                EditRefereeList.DOB = model.DOB;
                EditRefereeList.Length = model.Length;
                EditRefereeList.Weight = model.Weight;
                EditRefereeList.RefereeCommision = model.RefereeCommision;
                EditRefereeList.Mobile = model.Mobile;
                if (bytes != null)
                {
                    EditRefereeList.Photo = bytes;
                }
                db.SaveChanges();
            }
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();           

            return Content("<script>alert('Updated Successfully');location.href='RefereeView';</script>");
        }

    }
}
