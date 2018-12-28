using ClassicGarage.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.Models;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace ClassicGarage.Controllers
{
    public class HomeController : Controller
    {
        private GarageContext db = new GarageContext();
        public ActionResult Index() 
        {
            
            var e_mail = User.Identity.GetUserName();
            var query = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.ID).FirstOrDefault();
            Session["UserID"] = query;
            var car = db.Car.Include(p => p.Owner).Where(p=>p.OwnerID==query);
            var notice_exist = db.Notice.Include(p => p.Active).Where(p => p.Car.OwnerID == query).Where(p=>p.Active==true).Count();
            if(notice_exist>=1)
            {
                ViewBag.Notice = true;
            }
            //Console.WriteLine(string.Join(", ", cars));

            return View(car.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}