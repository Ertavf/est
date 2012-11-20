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
    public class FirmaController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /Firma/

        public ActionResult Index()
        {
            return View(db.Firmas.ToList());
        }

        //
        // GET: /Firma/Details/5

        public ActionResult Details(int id = 0)
        {
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        //
        // GET: /Firma/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Firma/Create

        [HttpPost]
        public ActionResult Create(Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Firmas.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(firma);
        }

        //
        // GET: /Firma/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        //
        // POST: /Firma/Edit/5

        [HttpPost]
        public ActionResult Edit(Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firma);
        }

        //
        // GET: /Firma/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        //
        // POST: /Firma/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Firma firma = db.Firmas.Find(id);
            db.Firmas.Remove(firma);
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