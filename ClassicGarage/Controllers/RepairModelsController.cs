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
            var Repair = db.Parts.Include(p => p.Repair).Where(p => p.RepairID == id);
            Session["RepairID"] = id;
            RepairModel nameRepair = db.Repair.Find(id);
            ViewBag.Description = nameRepair.Description;
            ViewBag.nameRepair = nameRepair.Name;
            ViewBag.Id = id;
            ViewBag.PriceRepair = nameRepair.Cost;
            
            if (Repair == null)
            {
                return HttpNotFound();
            }
            return View(Repair.ToList());
        }
        // GET: RepairModels/Repairs/5
        public ActionResult Repairs(int? id)
        {
            var Repair = db.Repair.Include(p => p.Car).Where(p => p.CarID == id);
            CarModel car = db.Car.Find(id);
            ViewBag.CarID = id;
            ViewBag.Car = car.Brand+" "+car.Model;
            return View(Repair.ToList());
        }

        // GET: RepairModels/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CarID = id;
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
                return RedirectToAction("Repairs",new { id = repairModel.CarID });
            }

            //ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", repairModel.CarID);
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
