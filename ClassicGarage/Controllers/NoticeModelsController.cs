using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;
using Microsoft.AspNet.Identity;

namespace ClassicGarage.Controllers
{
    public class NoticeModelsController : Controller
    {
        private GarageContext db = new GarageContext();
        //GET : NoticeModels/AllNotice
        public ActionResult AllNotice()
        {
            var notice = db.Notice.Include(n => n.Car).Include(n => n.Car.Owner).Where(n=>n.Active==true);
            return View(notice.ToList());
        }

        // GET: NoticeModels/5
        public ActionResult Index()
        {
            var temp =(int)Session["UserID"];
            var notice = db.Notice.Include(n => n.Car).Include(n => n.Car.Owner).Where(n => n.Car.Owner.ID == temp);
            return View(notice.ToList());
        }

        // GET: NoticeModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           //var temp = (int)Session["UserID"];
            var notice = db.Notice.Include(n => n.Car).Include(n => n.Car.Owner).Where(n => n.ID == id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice.ToList());
        }

        // GET: NoticeModels/Create
        public ActionResult Create()
        {
            var temp = (int)Session["UserID"];
            ViewBag.CarID = new SelectList(db.Car.Where(p=>p.OwnerID==temp), "ID", "Brand");
            return View();
        }

        // POST: NoticeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Description,Active,Price,Photo")] NoticeModel noticeModel)
        {
            string main = Server.MapPath("~/Content/Photo/");
            var e_mail = User.Identity.GetUserName();
            var firstname = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.FirstName).FirstOrDefault();
            var lastname = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.LastName).FirstOrDefault();

            string source = firstname + lastname + "\\";

            var TargetLocation = Path.Combine(main, source);
            if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = Request.Files["Photo"];
                if (postedFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(postedFile.FileName);
                    var path = Path.Combine(TargetLocation, fileName);
                    postedFile.SaveAs(path);
                    noticeModel.Photo = fileName;
                }
                else
                {
                    
                }
              
            }

            db.Notice.Add(noticeModel);
            db.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: NoticeModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticeModel noticeModel = db.Notice.Find(id);
            if (noticeModel == null)
            {
                return HttpNotFound();
            }
            var temp = (int)Session["UserID"];
            ViewBag.CarID = new SelectList(db.Car.Where(p=>p.OwnerID==temp), "ID", "Brand", noticeModel.CarID);
            return View(noticeModel);
        }

        // POST: NoticeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Description,Active,Price,Photo")] NoticeModel noticeModel)
        {
            if (ModelState.IsValid)
            {
                string main = Server.MapPath("~/Content/Photo/");
                var e_mail = User.Identity.GetUserName();
                var firstname = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.FirstName).FirstOrDefault();
                var lastname = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.LastName).FirstOrDefault();

                string source = firstname + lastname + "\\";

                var TargetLocation = Path.Combine(main, source);
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase postedFile = Request.Files["Photo"];
                    if (postedFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(postedFile.FileName);
                        var path = Path.Combine(TargetLocation, fileName);
                        postedFile.SaveAs(path);
                        noticeModel.Photo = fileName;
                    }

                }
                db.Entry(noticeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", noticeModel.CarID);
            return View(noticeModel);
        }

        // GET: NoticeModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticeModel noticeModel = db.Notice.Find(id);
            if (noticeModel == null)
            {
                return HttpNotFound();
            }
            return View(noticeModel);
        }

        // POST: NoticeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoticeModel noticeModel = db.Notice.Find(id);
            db.Notice.Remove(noticeModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
