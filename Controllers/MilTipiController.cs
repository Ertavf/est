﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class MilTipiController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /MilTipi/

        public ActionResult Index()
        {
            return View(db.MilTipis.ToList());
        }

        //
        // GET: /MilTipi/Details/5

        public ActionResult Details(int id = 0)
        {
            MilTipi miltipi = db.MilTipis.Find(id);
            if (miltipi == null)
            {
                return HttpNotFound();
            }
            return View(miltipi);
        }

        //
        // GET: /MilTipi/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MilTipi/Create

        [HttpPost]
        public ActionResult Create(MilTipi miltipi)
        {
            if (ModelState.IsValid)
            {
                db.MilTipis.Add(miltipi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(miltipi);
        }

        //
        // GET: /MilTipi/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MilTipi miltipi = db.MilTipis.Find(id);
            if (miltipi == null)
            {
                return HttpNotFound();
            }
            return View(miltipi);
        }

        //
        // POST: /MilTipi/Edit/5

        [HttpPost]
        public ActionResult Edit(MilTipi miltipi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(miltipi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(miltipi);
        }

        //
        // GET: /MilTipi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MilTipi miltipi = db.MilTipis.Find(id);
            if (miltipi == null)
            {
                return HttpNotFound();
            }
            return View(miltipi);
        }

        //
        // POST: /MilTipi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MilTipi miltipi = db.MilTipis.Find(id);
            db.MilTipis.Remove(miltipi);
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