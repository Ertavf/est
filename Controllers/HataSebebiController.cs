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
    public class HataSebebiController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        //
        // GET: /HataSebebi/

        public ActionResult Index()
        {
            return View(db.HataSebebis.ToList());
        }

        //
        // GET: /HataSebebi/Details/5

        public ActionResult Details(int id = 0)
        {
            HataSebebi hatasebebi = db.HataSebebis.Find(id);
            if (hatasebebi == null)
            {
                return HttpNotFound();
            }
            return View(hatasebebi);
        }

        //
        // GET: /HataSebebi/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HataSebebi/Create

        [HttpPost]
        public ActionResult Create(HataSebebi hatasebebi)
        {
            if (ModelState.IsValid)
            {
                db.HataSebebis.Add(hatasebebi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hatasebebi);
        }

        //
        // GET: /HataSebebi/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HataSebebi hatasebebi = db.HataSebebis.Find(id);
            if (hatasebebi == null)
            {
                return HttpNotFound();
            }
            return View(hatasebebi);
        }

        //
        // POST: /HataSebebi/Edit/5

        [HttpPost]
        public ActionResult Edit(HataSebebi hatasebebi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hatasebebi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hatasebebi);
        }

        //
        // GET: /HataSebebi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HataSebebi hatasebebi = db.HataSebebis.Find(id);
            if (hatasebebi == null)
            {
                return HttpNotFound();
            }
            return View(hatasebebi);
        }

        //
        // POST: /HataSebebi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HataSebebi hatasebebi = db.HataSebebis.Find(id);
            db.HataSebebis.Remove(hatasebebi);
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