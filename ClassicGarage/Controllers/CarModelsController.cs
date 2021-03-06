﻿using System;
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
    public class CarModelsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: CarModels
        public ActionResult Index()
        {
            var car = db.Car.Include(c => c.Owner);
            return View(car.ToList());
        }

        // GET: CarModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = db.Car.Find(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        // GET: CarModels/Create
        public ActionResult Create()
        {
            //ViewBag.OwnerID = new SelectList(db.Owner, "ID", "FirstName");
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "ID,Brand,Model,Year,VIN,Series,Photo,Buy_Date,Sell_Date,Buy_Cost,Sell_Cost,OwnerID")] CarModel carModel)
        {
           string main = Server.MapPath("~/Content/Photo/");
            var e_mail = User.Identity.GetUserName();
            var firstname = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.FirstName).FirstOrDefault();
            var lastname  = db.Owner.Where(s => s.EMail == e_mail).Select(s => s.LastName).FirstOrDefault();

            string source = firstname + lastname+"\\";

            var TargetLocation = Path.Combine(main, source);
            if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = Request.Files["Photo"];

              
                    if (postedFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(postedFile.FileName);
                        var path = Path.Combine(TargetLocation, fileName);
                        postedFile.SaveAs(path);
                    carModel.Photo = fileName;
                    }
               
               

                db.Car.Add(carModel);
                db.SaveChanges();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        
     

            //ViewBag.OwnerID = new SelectList(db.Owner, "ID", "FirstName", carModel.OwnerID);
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = db.Car.Find(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            // ViewBag.OwnerID = new SelectList(db.Owner, "ID", "FirstName", carModel.OwnerID);
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Brand,Model,Year,VIN,Series,Photo,Buy_Date,Sell_Date,Buy_Cost,Sell_Cost,OwnerID")] CarModel carModel)
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
                    carModel.Photo = fileName;
                }

                db.Entry(carModel).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            //ViewBag.OwnerID = new SelectList(db.Owner, "ID", "FirstName", carModel.OwnerID);
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = db.Car.Find(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModel carModel = db.Car.Find(id);
            //biore wszystkie id 
            var temp = db.Repair.Where(p => p.CarID == id);
            foreach (var item in temp)
            {
                //bierzemy wszystkie czesci dla danej naprawy
                var part = db.Parts.Where(x => x.RepairID == item.ID);
                //ustawiamy na nulle wszystkie repair id
                    foreach(var item_1 in part)
                            {
                                 item_1.RepairID = null;
                            }
             
            }
            //kasujemy naprawy
                 db.Repair.RemoveRange(db.Repair.Where(x => x.CarID == id)).Select(p=>p.ID);
            
            db.Car.Remove(carModel);
            db.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
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
