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
    public class TezgahController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /Tezgah/

        public ActionResult Index()
        {
            return View(db.Tezgahs.ToList());
        }

        //
        // GET: /Tezgah/Details/5

        public ActionResult Details(int id = 0)
        {
            Tezgah tezgah = db.Tezgahs.Find(id);
            if (tezgah == null)
            {
                return HttpNotFound();
            }
            return View(tezgah);
        }

        //
        // GET: /Tezgah/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tezgah/Create

        [HttpPost]
        public ActionResult Create(Tezgah tezgah)
        {
            if (ModelState.IsValid)
            {
                db.Tezgahs.Add(tezgah);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tezgah);
        }

        //
        // GET: /Tezgah/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Tezgah tezgah = db.Tezgahs.Find(id);
            if (tezgah == null)
            {
                return HttpNotFound();
            }
            return View(tezgah);
        }

        //
        // POST: /Tezgah/Edit/5

        [HttpPost]
        public ActionResult Edit(Tezgah tezgah)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tezgah).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tezgah);
        }

        //
        // GET: /Tezgah/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Tezgah tezgah = db.Tezgahs.Find(id);
            if (tezgah == null)
            {
                return HttpNotFound();
            }
            return View(tezgah);
        }

        //
        // POST: /Tezgah/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tezgah tezgah = db.Tezgahs.Find(id);
            db.Tezgahs.Remove(tezgah);
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