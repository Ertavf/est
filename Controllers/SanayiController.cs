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
    public class SanayiController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /Sanayi/

        public ActionResult Index()
        {
            return View(db.Sanayis.ToList());
        }

        //
        // GET: /Sanayi/Details/5

        public ActionResult Details(int id = 0)
        {
            Sanayi sanayi = db.Sanayis.Find(id);
            if (sanayi == null)
            {
                return HttpNotFound();
            }
            return View(sanayi);
        }

        //
        // GET: /Sanayi/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Sanayi/Create

        [HttpPost]
        public ActionResult Create(Sanayi sanayi)
        {
            if (ModelState.IsValid)
            {
                db.Sanayis.Add(sanayi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanayi);
        }

        //
        // GET: /Sanayi/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sanayi sanayi = db.Sanayis.Find(id);
            if (sanayi == null)
            {
                return HttpNotFound();
            }
            return View(sanayi);
        }

        //
        // POST: /Sanayi/Edit/5

        [HttpPost]
        public ActionResult Edit(Sanayi sanayi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanayi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanayi);
        }

        //
        // GET: /Sanayi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sanayi sanayi = db.Sanayis.Find(id);
            if (sanayi == null)
            {
                return HttpNotFound();
            }
            return View(sanayi);
        }

        //
        // POST: /Sanayi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Sanayi sanayi = db.Sanayis.Find(id);
            db.Sanayis.Remove(sanayi);
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