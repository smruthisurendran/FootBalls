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
    public class ChampionshipSponsorDetailsController : Controller
    {
        AllUsersContext db = new AllUsersContext();

        // GET: ChampionshipSponsor
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChampionshipSponsor(int? page)
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

           List<TblChampionshipSponsor> championshipsponsorinfo = db.ChampionshipSponsor_tbl.OrderByDescending(x => x.CreatedDate).ToList();

            if (championshipsponsorinfo != null)
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(championshipsponsorinfo.ToPagedList(pageNumber, pageSize));
            }       

            return View();
        }

        [HttpGet]
        public ActionResult ChampionshipSponsorRegistration()
        {
            
            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            return View();
            
        }

        [HttpPost]
        public ActionResult ChampionshipSponsorRegistration(TblChampionshipSponsor model,string Category)
        {
            var userid = Session["UserId"].ToString();

            List<TblUser> user = db.User_tbl.ToList();
            ViewBag.UserList = new SelectList(user, "UserId", "UserId");

            if (ModelState.IsValid)
            {
               
                db.ChampionshipSponsor_tbl.Add(new TblChampionshipSponsor
                {
                    ChampionshipSponsorReferenceNumber = "1",
                  
                    Name = model.Name,
                    Category = model.Category,
                    Confirmed = 1,

                    RegistrationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now,

                    Mobile = model.Mobile,
                    UserId = Convert.ToInt32(userid),

                    Status = 1,
                    CreatedId = 1,
                    CreatedDate = DateTime.Now,

                    ModifiedId = 0,
                    ModifiedDate = DateTime.Now
                });


                db.SaveChanges();



                return RedirectToAction("ChampionshipSponsor");
            }
            return View();

        }

      
        [HttpGet]
        public ActionResult ChampionshipSponsorView(int id)
        {
            if (id != 0)
            {

                return View(db.ChampionshipSponsor_tbl.Where(x => x.ChampionshipSponsorId == id && x.Status == 1).FirstOrDefault());
            }
            return View();
        }
    }
}