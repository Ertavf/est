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
    public class EstasKoduTipiController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /EstasKoduTipi/

        public ActionResult Index()
        {
            return View(db.EstasKoduTipis.ToList());
        }

        //
        // GET: /EstasKoduTipi/Details/5

        public ActionResult Details(int id = 0)
        {
            EstasKoduTipi estaskodutipi = db.EstasKoduTipis.Find(id);
            if (estaskodutipi == null)
            {
                return HttpNotFound();
            }
            return View(estaskodutipi);
        }

        //
        // GET: /EstasKoduTipi/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EstasKoduTipi/Create

        [HttpPost]
        public ActionResult Create(EstasKoduTipi estaskodutipi)
        {
            if (ModelState.IsValid)
            {
                db.EstasKoduTipis.Add(estaskodutipi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estaskodutipi);
        }

        //
        // GET: /EstasKoduTipi/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EstasKoduTipi estaskodutipi = db.EstasKoduTipis.Find(id);
            if (estaskodutipi == null)
            {
                return HttpNotFound();
            }
            return View(estaskodutipi);
        }

        //
        // POST: /EstasKoduTipi/Edit/5

        [HttpPost]
        public ActionResult Edit(EstasKoduTipi estaskodutipi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estaskodutipi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estaskodutipi);
        }

        //
        // GET: /EstasKoduTipi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EstasKoduTipi estaskodutipi = db.EstasKoduTipis.Find(id);
            if (estaskodutipi == null)
            {
                return HttpNotFound();
            }
            return View(estaskodutipi);
        }

        //
        // POST: /EstasKoduTipi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EstasKoduTipi estaskodutipi = db.EstasKoduTipis.Find(id);
            db.EstasKoduTipis.Remove(estaskodutipi);
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