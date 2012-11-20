using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class HataYakalayanBolumController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /HataYakalayanBolum/

        public ActionResult Index()
        {
            return View(db.HataYakalayanBolums.ToList());
        }

        //
        // GET: /HataYakalayanBolum/Details/5

        public ActionResult Details(int id = 0)
        {
            HataYakalayanBolum hatayakalayanbolum = db.HataYakalayanBolums.Find(id);
            if (hatayakalayanbolum == null)
            {
                return HttpNotFound();
            }
            return View(hatayakalayanbolum);
        }

        //
        // GET: /HataYakalayanBolum/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HataYakalayanBolum/Create

        [HttpPost]
        public ActionResult Create(HataYakalayanBolum hatayakalayanbolum)
        {
            if (ModelState.IsValid)
            {
                db.HataYakalayanBolums.Add(hatayakalayanbolum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hatayakalayanbolum);
        }

        //
        // GET: /HataYakalayanBolum/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HataYakalayanBolum hatayakalayanbolum = db.HataYakalayanBolums.Find(id);
            if (hatayakalayanbolum == null)
            {
                return HttpNotFound();
            }
            return View(hatayakalayanbolum);
        }

        //
        // POST: /HataYakalayanBolum/Edit/5

        [HttpPost]
        public ActionResult Edit(HataYakalayanBolum hatayakalayanbolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hatayakalayanbolum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hatayakalayanbolum);
        }

        //
        // GET: /HataYakalayanBolum/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HataYakalayanBolum hatayakalayanbolum = db.HataYakalayanBolums.Find(id);
            if (hatayakalayanbolum == null)
            {
                return HttpNotFound();
            }
            return View(hatayakalayanbolum);
        }

        //
        // POST: /HataYakalayanBolum/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HataYakalayanBolum hatayakalayanbolum = db.HataYakalayanBolums.Find(id);
            db.HataYakalayanBolums.Remove(hatayakalayanbolum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}