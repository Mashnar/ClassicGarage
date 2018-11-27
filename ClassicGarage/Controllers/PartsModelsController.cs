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
    public class PartsModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: PartsModels
        public ActionResult Index()
        {
            var parts = db.Parts.Include(p => p.Repair);
            return View(parts.ToList());
        }

        // GET: PartsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartsModel partsModel = db.Parts.Find(id);
            if (partsModel == null)
            {
                return HttpNotFound();
            }
            return View(partsModel);
        }

        // GET: PartsModels/Create/5
        public ActionResult Create(int? id)
        {
            ViewBag.num = id;
            return View();
        }

        // POST: PartsModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RepairID,Name,Cost_Buy,Cost_Sell,Buy_Date,Sell_Date,RepairID")] PartsModel partsModel)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(partsModel);
               

                var result = db.Repair.SingleOrDefault(s => s.ID == partsModel.RepairID);
                if(result != null)
                {
                    result.Cost = (int)partsModel.Cost_Buy;
                       
                }
                db.SaveChanges();
                return RedirectToAction("Details", "RepairModels");
            }

           // ViewBag.RepairID = new SelectList(db.Repair, "ID", "Name", partsModel.RepairID);
            return View(partsModel);
        }
     

        // GET: PartsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartsModel partsModel = db.Parts.Find(id);
            if (partsModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.RepairID = new SelectList(db.Repair, "ID", "Name", partsModel.RepairID);
            return View(partsModel);
        }

        // POST: PartsModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RepairID,Name,Cost_Buy,Cost_Sell,Buy_Date,Sell_Date")] PartsModel partsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RepairID = new SelectList(db.Repair, "ID", "Name", partsModel.RepairID);
            return View(partsModel);
        }


       

        // GET: PartsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartsModel partsModel = db.Parts.Find(id);
            if (partsModel == null)
            {
                return HttpNotFound();
            }
            return View(partsModel);
        }

        // POST: PartsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartsModel partsModel = db.Parts.Find(id);
            db.Parts.Remove(partsModel);
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
