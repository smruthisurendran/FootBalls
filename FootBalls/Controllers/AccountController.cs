using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FootBalls.Models;
using System.Data;
using System.Net;
using System.Web.Routing;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FootBalls.Controllers
{
    public class AccountController : Controller
    {
        AllUsersContext db = new AllUsersContext();


        public object GeneralReferenceNumber { get; private set; }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(TblUser model, string Category)
        {

            TblUser tblUser = new TblUser();
            tblUser.UserId = model.UserId;
            tblUser.GeneralReferenceNumber = Convert.ToInt32(GeneralReferenceNumber);
            tblUser.EmailId = model.EmailId;
            tblUser.Category = Convert.ToInt32(Category);
            tblUser.Name = model.Name;
            tblUser.MobileNumber = model.MobileNumber;
            tblUser.Password = model.Password;
            tblUser.Status = 1;
            tblUser.CreatedId = 1;
            tblUser.CreatedDate = System.DateTime.Now;
            tblUser.ModifiedId = 0;
            tblUser.ModifiedDate = DateTime.Now;
            db.User_tbl.Add(tblUser);
            db.SaveChanges();

            return Content("<script>alert('Registration Successfull');location.href='Login';</script>");
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TblUser model)
        {

            if (ModelState.IsValid)
            {

                var user = (from userlist in db.User_tbl
                            where userlist.EmailId == model.EmailId && userlist.Password == model.Password
                            select new
                            {
                                userlist.UserId,
                                userlist.EmailId,
                                userlist.Name,
                                userlist.MobileNumber

                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    Session["EmailId"] = user.FirstOrDefault().EmailId;
                    Session["UserId"] = user.FirstOrDefault().UserId;
                    Session["Name"] = user.FirstOrDefault().Name;
                    Session["MobileNumber"] = user.FirstOrDefault().MobileNumber;
                    return RedirectToAction("LoginMain", "Account");
                }
                else
                {

                    return Content("<script type='text/javascript'>window.alert('Invalid login');window.location.href='';</script>");
                    //  return RedirectToAction("Login", "Account");

                }
            }

            return View();
        }
        public ActionResult Player()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult LoginMain()
        {
            var UserId = Session["UserId"].ToString();
            int Userid = Convert.ToInt32(UserId);
            if (Userid != 0)
            {
                return View(db.User_tbl.Where(x => x.UserId == Userid && x.Status == 1).FirstOrDefault());
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangeBackground()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ChangeBackground(TblChangeBackground model, string UserRole, HttpPostedFileBase Image)
        {

               TblChangeBackground tblChangeBackground = new TblChangeBackground();
               tblChangeBackground.UserRole = model.UserRole;

                   string fileName = Path.GetFileName(Image.FileName);
                   string filePath = "~/WebImages/" + fileName;
                   Image.SaveAs(Server.MapPath(filePath));

               db.ChangeBackground_tbl.Add(tblChangeBackground);
               db.SaveChanges();

              if(model.UserRole==1)
               {
                   Session["PlayerbackgroundImage"] = "../../WebImages/" + fileName;
               } 

              if(model.UserRole==2)
               {
                   Session["CoachbackgroundImage"] = "../../WebImages/" + fileName;
               }

               if (model.UserRole == 3)
               {
                   Session["TeambackgroundImage"] = "../../WebImages/" + fileName;
               }

               return RedirectToAction("Login");
           }
           

    }
}