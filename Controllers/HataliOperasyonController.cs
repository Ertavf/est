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
    public class HataliOperasyonController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /HataliOperasyon/

        public ActionResult Index()
        {
            return View(db.HataliOperasyons.ToList());
        }

        //
        // GET: /HataliOperasyon/Details/5

        public ActionResult Details(int id = 0)
        {
            HataliOperasyon hatalioperasyon = db.HataliOperasyons.Find(id);
            if (hatalioperasyon == null)
            {
                return HttpNotFound();
            }
            return View(hatalioperasyon);
        }

        //
        // GET: /HataliOperasyon/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HataliOperasyon/Create

        [HttpPost]
        public ActionResult Create(HataliOperasyon hatalioperasyon)
        {
            if (ModelState.IsValid)
            {
                db.HataliOperasyons.Add(hatalioperasyon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hatalioperasyon);
        }

        //
        // GET: /HataliOperasyon/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HataliOperasyon hatalioperasyon = db.HataliOperasyons.Find(id);
            if (hatalioperasyon == null)
            {
                return HttpNotFound();
            }
            return View(hatalioperasyon);
        }

        //
        // POST: /HataliOperasyon/Edit/5

        [HttpPost]
        public ActionResult Edit(HataliOperasyon hatalioperasyon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hatalioperasyon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hatalioperasyon);
        }

        //
        // GET: /HataliOperasyon/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HataliOperasyon hatalioperasyon = db.HataliOperasyons.Find(id);
            if (hatalioperasyon == null)
            {
                return HttpNotFound();
            }
            return View(hatalioperasyon);
        }

        //
        // POST: /HataliOperasyon/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HataliOperasyon hatalioperasyon = db.HataliOperasyons.Find(id);
            db.HataliOperasyons.Remove(hatalioperasyon);
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