using ClassicGarage.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
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