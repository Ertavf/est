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
    public class HataliBolumController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /HataliBolum/

        public ActionResult Index()
        {
            return View(db.HataliBolums.ToList());
        }

        //
        // GET: /HataliBolum/Details/5

        public ActionResult Details(int id = 0)
        {
            HataliBolum hatalibolum = db.HataliBolums.Find(id);
            if (hatalibolum == null)
            {
                return HttpNotFound();
            }
            return View(hatalibolum);
        }

        //
        // GET: /HataliBolum/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HataliBolum/Create

        [HttpPost]
        public ActionResult Create(HataliBolum hatalibolum)
        {
            if (ModelState.IsValid)
            {
                db.HataliBolums.Add(hatalibolum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hatalibolum);
        }

        //
        // GET: /HataliBolum/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HataliBolum hatalibolum = db.HataliBolums.Find(id);
            if (hatalibolum == null)
            {
                return HttpNotFound();
            }
            return View(hatalibolum);
        }

        //
        // POST: /HataliBolum/Edit/5

        [HttpPost]
        public ActionResult Edit(HataliBolum hatalibolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hatalibolum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hatalibolum);
        }

        //
        // GET: /HataliBolum/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HataliBolum hatalibolum = db.HataliBolums.Find(id);
            if (hatalibolum == null)
            {
                return HttpNotFound();
            }
            return View(hatalibolum);
        }

        //
        // POST: /HataliBolum/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HataliBolum hatalibolum = db.HataliBolums.Find(id);
            db.HataliBolums.Remove(hatalibolum);
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