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
    public class HataController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        private IQueryable<Hata> Hatas
        {
            get
            {
                return db.Hatas.Include(p => p.HataliBolum).Include(p => p.HataSebebi).Include(p => p.HataAdet).Include(p=>p.HataYakalayanBolum);
            }
        }
        //
        // GET: /Hata/

        public ActionResult Index(int id = 0)
        {
            var hatas = db.Hatas.Include(h => h.Parti).Include(h => h.HataSebebi).Include(h => h.HataliBolum).Include(h => h.HataYakalayanBolum).Include(h => h.Tezgah);
            if (id != 0)
            {
                hatas = hatas.Where(m => m.HataId == id);
            }
            return View(hatas.ToList());
        }

        //
        // GET: /Hata/Details/5

        public ActionResult Details(int id = 0)
        {
            Hata hata = db.Hatas.Find(id);
            if (hata == null)
            {
                return HttpNotFound();
            }
            return View(hata);
        }

        
        //
        // GET: /Hata/Create

       public ActionResult Create()
        {
            ViewBag.PartiId = new SelectList(db.Partis, "PartiId", "PartiKodu");
            ViewBag.HataSebebiId = new SelectList(db.HataSebebis, "HataSebebiId", "Sebep");
            ViewBag.HataliBolumId = new SelectList(db.HataliBolums, "HataliBolumId", "BolumIsmi");
            ViewBag.HataYakalayanBolumId = new SelectList(db.HataYakalayanBolums, "HataYakalayanBolumId", "BolumIsmi");
            ViewBag.TezgahId = new SelectList(db.Tezgahs, "TezgahId", "TezgahKodu");
            return View();
        }  

        //
        // POST: /Hata/Create
       
        [HttpPost]
        public ActionResult Create(Hata hata)
        {
            if (ModelState.IsValid)
            {
                db.Hatas.Add(hata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartiId = new SelectList(db.Partis, "PartiId", "PartiKodu", hata.PartiId);
            ViewBag.HataSebebiId = new SelectList(db.HataSebebis, "HataSebebiId", "Sebep", hata.HataSebebiId);
            ViewBag.HataliBolumId = new SelectList(db.HataliBolums, "HataliBolumId", "BolumIsmi", hata.HataliBolumId);
            ViewBag.HataYakalayanBolumId = new SelectList(db.HataYakalayanBolums, "HataYakalayanBolumId", "BolumIsmi", hata.HataYakalayanBolumId);
            ViewBag.TezgahId = new SelectList(db.Tezgahs, "TezgahId", "TezgahKodu", hata.TezgahId);
            return View(hata);
        }    

        //
        // GET: /Hata/Edit/5
        
        public ActionResult Edit(int id = 0)
        {
            Hata hata = db.Hatas.Find(id);
            if (hata == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartiId = new SelectList(db.Partis, "PartiId", "PartiKodu", hata.PartiId);
            ViewBag.HataSebebiId = new SelectList(db.HataSebebis, "HataSebebiId", "Sebep", hata.HataSebebiId);
            ViewBag.HataliBolumId = new SelectList(db.HataliBolums, "HataliBolumId", "BolumIsmi", hata.HataliBolumId);
            ViewBag.HataYakalayanBolumId = new SelectList(db.HataYakalayanBolums, "HataYakalayanBolumId", "BolumIsmi", hata.HataYakalayanBolumId);
            ViewBag.TezgahId = new SelectList(db.Tezgahs, "TezgahId", "TezgahKodu", hata.TezgahId);
            return View(hata);
        }

        //
        // POST: /Hata/Edit/5

        [HttpPost]
        public ActionResult Edit(Hata hata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PartiId = new SelectList(db.Partis, "PartiId", "PartiKodu", hata.PartiId);
            ViewBag.HataSebebiId = new SelectList(db.HataSebebis, "HataSebebiId", "Sebep", hata.HataSebebiId);
            ViewBag.HataliBolumId = new SelectList(db.HataliBolums, "HataliBolumId", "BolumIsmi", hata.HataliBolumId);
            ViewBag.HataYakalayanBolumId = new SelectList(db.HataYakalayanBolums, "HataYakalayanBolumId", "BolumIsmi", hata.HataYakalayanBolumId);
            ViewBag.TezgahId = new SelectList(db.Tezgahs, "TezgahId", "TezgahKodu", hata.TezgahId);
            return View(hata);
        }   
   
        //
        // GET: /Hata/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Hata hata = db.Hatas.Find(id);
            if (hata == null)
            {
                return HttpNotFound();
            }
            return View(hata);
        }

        //
        // POST: /Hata/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Hata hata = db.Hatas.Find(id);
            db.Hatas.Remove(hata);
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