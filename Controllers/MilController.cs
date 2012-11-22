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
    public class MilController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();


        public IQueryable<Mil> Mils 
        {
            get{
                return db.Mils.Include(m => m.Sanayi).Include(m => m.MilTipi).Include(m => m.MilPartis).Include(m => m.Firma).Include(m => m.EstasKoduTipi).Include(m => m.TaniticiRenk);
            }
        }

        //
        // GET: /Mil/

        public ActionResult Index(int MilId = 0, int bas = 0, int getir = 5)
        {
            if (bas < 0)
                bas = 0;
            @ViewBag.Toplam = Mils.Count();

            var mils = Mils.OrderByDescending(p => p.MilKodu).Skip(bas).Take(getir);
            ViewBag.Bas = bas;
            ViewBag.Getir = getir;
            if (MilId != 0)
            {
                mils = mils.Where(p => p.MilId == MilId);
            }


            List<Mil> miller = mils.ToList();
            //partiler.ForEach(p => p.isValid = TryValidateModel(p));

            foreach (Mil ml in miller)
            {
                ModelMetadata modelMetaData = ModelMetadataProviders.Current.GetMetadataForType(() => ml, typeof(Mil));
                //prti.isValid = ModelValidator.GetModelValidator(modelMetaData, ControllerContext);
            }
            return View(miller);
            /*var mils = Mils;

            if (id != 0)
            {
                mils = mils.Where(m => m.MilId == id);
            }
            return View(mils.ToList());  */
        }

        //
        // GET: /Mil/Details/5
        /*
        public ActionResult Details(int id = 0)
        {
            Mil mil = db.Mils.Find(id);
            if (mil == null)
            {
                return HttpNotFound();
            }
            return View(mil);
        }
        */
        
        //
        // GET: /Mil/Create

        public ActionResult Create()
        {
            return RedirectToAction("Duzenle");
        }
        public ActionResult Edit(int id = 0)
        {
            return RedirectToAction("Duzenle", new { id = id});
        }
/*
        //
        // POST: /Mil/Create

        [HttpPost]
        public ActionResult Create(Mil mil)
        {
            if (ModelState.IsValid)
            {
                db.Mils.Add(mil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SanayiId = new SelectList(db.Sanayis, "SanayiId", "SanayiTipi", mil.SanayiId);
            return View(mil);
        }
        */
        //
        // GET: /Mil/Edit/5

        public ActionResult Duzenle(int id = 0)
        {
            Mil mil;
            if (id == 0)//yeni olustur
            {
                mil = new Mil();
            }
            else
            {
                mil = Mils.Single(m => m.MilId == id);
            }
            if (mil == null)
            {
                return HttpNotFound();
            }
            SetSelectLists(mil);
            return View(mil);
        }

        private void SetSelectLists(Mil mil)
        {
            ViewBag.SanayiId = new SelectList(db.Sanayis, "SanayiId", "SanayiTipi", mil.SanayiId);
            ViewBag.MilTipiId = new SelectList(db.MilTipis, "MilTipiId", "MilTipiIsmi", mil.MilTipiId);
            ViewBag.FirmaId = new SelectList(db.Firmas, "FirmaId", "FirmaAramaStr", mil.FirmaId);
            ViewBag.EstasKoduTipiId = new SelectList(db.EstasKoduTipis, "EstasKoduTipiId", "EstasKoduTipiStr", mil.EstasKoduTipiId);
            ViewBag.TaniticiRenkId = new SelectList(db.TaniticiRenks, "TaniticiRenkId", "TaniticiRenkStr", mil.TaniticiRenkId);
        }

        //
        // POST: /Mil/Edit/5
        [HttpPost]
        public ActionResult Duzenle(Mil mil)
        {
            if (mil.MilTipiId == 1 && ((mil.DokumMil_ModelNo == null) || mil.DokumMil_ModelNo.Length < 2))
            {
                ModelState.AddModelError("DokumMil_ModelNo", "Döküm Millerinin Model Numarası olmalı");
            }
            if (ModelState.IsValid)
            {
                if (mil.MilId == 0)//yeni olustur
                {
                    db.Mils.Add(mil);
                }
                else
                {
                    db.Entry(mil).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SetSelectLists(mil); 
            return View(mil);
        }

        //
        // GET: /Mil/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Mil mil = db.Mils.Find(id);
            if (mil == null)
            {
                return HttpNotFound();
            }
            return View(mil);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Mil mil = db.Mils.Find(id);
            db.Mils.Remove(mil);
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