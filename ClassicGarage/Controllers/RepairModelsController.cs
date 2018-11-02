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
    public class RepairModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: RepairModels
        public ActionResult Index()
        {
            var repair = db.Repair.Include(r => r.Car);
            return View(repair.ToList());
        }

        // GET: RepairModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModel repairModel = db.Repair.Find(id);
            if (repairModel == null)
            {
                return HttpNotFound();
            }
            return View(repairModel);
        }

        // GET: RepairModels/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand");
            return View();
        }

        // POST: RepairModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Name,Description,Cost")] RepairModel repairModel)
        {
            if (ModelState.IsValid)
            {
                db.Repair.Add(repairModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", repairModel.CarID);
            return View(repairModel);
        }

        // GET: RepairModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModel repairModel = db.Repair.Find(id);
            if (repairModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", repairModel.CarID);
            return View(repairModel);
        }

        // POST: RepairModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Name,Description,Cost")] RepairModel repairModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", repairModel.CarID);
            return View(repairModel);
        }

        // GET: RepairModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModel repairModel = db.Repair.Find(id);
            if (repairModel == null)
            {
                return HttpNotFound();
            }
            return View(repairModel);
        }

        // POST: RepairModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairModel repairModel = db.Repair.Find(id);
            db.Repair.Remove(repairModel);
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
