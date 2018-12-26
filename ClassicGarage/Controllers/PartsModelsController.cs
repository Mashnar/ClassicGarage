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
        public ActionResult Index(int ?id)
        {
            if(id== null)
            {
                var parts = db.Parts.Include(p => p.Repair);
                return View(parts.ToList());
            }
            else
            {
                var parts = db.Parts.Include(p => p.Repair).Where(p => p.Repair.CarID == id);
                return View(parts.ToList());
            }
            //includuje czdesci do napraw gdzie id auta w napraiwe to id 
            
           
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
                  if(result.Cost == null)
                    {
                        result.Cost = 0;
                    }
                    result.Cost = partsModel.Cost_Buy+result.Cost;
                       
                }
                db.SaveChanges();
                return RedirectToAction("Details", "RepairModels",new { id=partsModel.RepairID});
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
            var temp = (int)Session["UserID"];
            var RepairID = new SelectList(db.Repair.Where(p=>p.Car.OwnerID== temp), "ID", "Name", partsModel.RepairID).ToList();
            RepairID.Insert(0, (new SelectListItem { Text = "Żadna", Value = "0" }));
            ViewBag.RepairID = RepairID;

            return View(partsModel);
        }

        // POST: PartsModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RepairID,Name,Cost_Buy,Cost_Sell,Buy_Date,Sell_Date")] PartsModel partsModel)
        {
           
            //db.Entry(partsModel).State = EntityState.Modified;
            if (partsModel.RepairID == 0)//wiemy ze nie ma zadnej naprawy
            {
                var RepairID = db.Parts.Find(partsModel.ID);
                var temp = RepairID.RepairID;
                var repair = db.Repair.Find(temp);
                repair.Cost = repair.Cost - partsModel.Cost_Buy;
                RepairID.RepairID = null;
                RepairID.Sell_Date = partsModel.Sell_Date;
                RepairID.Cost_Sell = partsModel.Cost_Sell;
                



                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index", "PartsModels", null);
                }
            }
            else//wiemy ze zmieniamy na jakas naprawe takze przypisujemy 
            {
                var NewRepair = db.Repair.Find(partsModel.RepairID);//nowa naprawa
                var RepairID = db.Parts.Find(partsModel.ID);
                var temp = RepairID.RepairID;
                var OldRepair = db.Repair.Find(temp);
                if(OldRepair == null)
                {
                    NewRepair.Cost = NewRepair.Cost + partsModel.Cost_Buy;
                }
                else
                {
                    NewRepair.Cost = NewRepair.Cost + partsModel.Cost_Buy;
                    OldRepair.Cost = OldRepair.Cost - partsModel.Cost_Buy;
                }
              
          

           
                RepairID.RepairID = partsModel.RepairID;

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index", "PartsModels", null);
                }

            }
          
            //ViewBag.RepairID = new SelectList(db.Repair, "ID", "Name", partsModel.RepairID);
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
      
            var repair = db.Repair.Find(partsModel.RepairID);
            repair.Cost = repair.Cost - partsModel.Cost_Buy;
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
