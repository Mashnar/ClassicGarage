using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;

namespace ClassicGarage.Controllers
{
    public class NoticeModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: NoticeModels
        public ActionResult Index()
        {
            var notice = db.Notice.Include(n => n.Car);
            return View(notice.ToList());
        }

        // GET: NoticeModels/Details/5
        public ActionResult Details(int? id)
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

        // GET: NoticeModels/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Car, "ID", "Brand");
            return View();
        }

        // POST: NoticeModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Active")] NoticeModel noticeModel)
        {
            if (ModelState.IsValid)
            {
                db.Notice.Add(noticeModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Car, "ID", "Brand", noticeModel.ID);
            return View(noticeModel);
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
            ViewBag.ID = new SelectList(db.Car, "ID", "Brand", noticeModel.ID);
            return View(noticeModel);
        }

        // POST: NoticeModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Active")] NoticeModel noticeModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Car, "ID", "Brand", noticeModel.ID);
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
