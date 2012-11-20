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
    public class PersonelController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /Personel/

        public ActionResult Index()
        {
            var personels = db.Personels.Include(p => p.Bolum);
            return View(personels.ToList());
        }

        //
        // GET: /Personel/Create

        public ActionResult Create()
        {
            ViewBag.BolumId = new SelectList(db.Bolums, "BolumId", "BolumAdi");
            return View();
        }

        //
        // POST: /Personel/Create

        [HttpPost]
        public ActionResult Create(Personel personel)
        {
            if (ModelState.IsValid)
            {
                db.Personels.Add(personel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BolumId = new SelectList(db.Bolums, "BolumId", "BolumAdi", personel.BolumId);
            return View(personel);
        }

        //
        // GET: /Personel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Personel personel = db.Personels.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "BolumId", "BolumAdi", personel.BolumId);
            return View(personel);
        }

        //
        // POST: /Personel/Edit/5

        [HttpPost]
        public ActionResult Edit(Personel personel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BolumId = new SelectList(db.Bolums, "BolumId", "BolumAdi", personel.BolumId);
            return View(personel);
        }

        //
        // GET: /Personel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Personel personel = db.Personels.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return View(personel);
        }

        //
        // POST: /Personel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Personel personel = db.Personels.Find(id);
            db.Personels.Remove(personel);
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