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
using Microsoft.AspNet.Identity;

namespace ClassicGarage.Controllers
{
    public class OwnerModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: OwnerModels
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        // GET: OwnerModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerModel ownerModel = db.Owner.Find(id);
            if (ownerModel == null)
            {
                return HttpNotFound();
            }
            return View(ownerModel);
        }

        // GET: OwnerModels/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnerModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Phone,EMail")] OwnerModel ownerModel)
        {
            if (ModelState.IsValid)
            {
                db.Owner.Add(ownerModel);
                db.SaveChanges();
                var e_mail = User.Identity.GetUserName();
                var UserID = db.Owner.Where(p => p.EMail == e_mail).Select(p => p.ID).FirstOrDefault();
                Session["UserID"] = UserID;
                return RedirectToAction("Index");
            }

            return View(ownerModel);
        }

        // GET: OwnerModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerModel ownerModel = db.Owner.Find(id);
            if (ownerModel == null)
            {
                return HttpNotFound();
            }
            return View(ownerModel);
        }

        // POST: OwnerModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Phone,EMail")] OwnerModel ownerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ownerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownerModel);
        }

        // GET: OwnerModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerModel ownerModel = db.Owner.Find(id);
            if (ownerModel == null)
            {
                return HttpNotFound();
            }
            return View(ownerModel);
        }

        // POST: OwnerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerModel ownerModel = db.Owner.Find(id);
            db.Owner.Remove(ownerModel);
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
