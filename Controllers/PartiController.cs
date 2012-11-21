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
    public class PartiController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();


        private IQueryable<Parti> Partis
        {
            get
            {
                return db.Partis.Include(p => p.Mil).Include(p => p.Mil.Sanayi).Include(p => p.Mil.MilTipi).Include(m => m.Mil.Firma).Include(m => m.Mil.EstasKoduTipi).Include(m=>m.Hatas);
            }
        }
        public IQueryable<Mil> Mils
        {
            get
            {
                return db.Mils.Include(m => m.Sanayi).Include(m => m.MilTipi).Include(m => m.MilPartis).Include(m => m.Firma).Include(m => m.EstasKoduTipi);
            }
        }

        //
        // GET: /Parti/
        public ActionResult Index(int MilId = 0, int bas = 0, int getir = 5)
        {
            if (bas < 0)
                bas = 0;
            @ViewBag.Toplam = Partis.Count();

            var partis = Partis.OrderByDescending(p => p.IsEmriNo).Skip(bas).Take(getir);
            ViewBag.Bas = bas;
            ViewBag.Getir = getir;
            if (MilId != 0)
            {
                partis = partis.Where(p => p.MilId == MilId);
            }


            List<Parti> partiler = partis.ToList();
            //partiler.ForEach(p => p.isValid = TryValidateModel(p));

            foreach (Parti prti in partiler)
            {
                ModelMetadata modelMetaData=  ModelMetadataProviders.Current.GetMetadataForType(() => prti, typeof(Parti));
                //prti.isValid = ModelValidator.GetModelValidator(modelMetaData, ControllerContext);
            }
            return View(partiler);
        }

        //
        // GET: /Parti/Details/5
        ///////////////////////                   COMMENT KALSIN EXCEL CIKTISI VERIYOR
        /*
        public ActionResult Details(int id = 0)
        {
            Parti parti = db.Partis.Include(p => p.Mil).Single(p => p.PartiId == id);
            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }*/
        public ActionResult Form(int id = 0, int form = 0)
        {
            Parti parti = Partis.Single(p => p.PartiId == id);
            switch(form){
                case 1:
                    ViewBag.FormCss = Url.Content("~/Content/Formlar/kaliteKontrolForm.css");
                    break;
                default:
                    ViewBag.FormCss = Url.Content("~/Content/Formlar/imalatIsEmriForm.css");
                    break;
        }
            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }
        
        [ChildActionOnly]
        public string SiradakiPartiSirasi(){
            try
            {
                int maxPartiId = db.Partis.Max(p => p.PartiId);
                Parti sonParti = db.Partis.Single(p => p.PartiId == maxPartiId);
                string sonPartiKodu = sonParti.PartiKodu;
                char partiSirasi = '1';
                if (sonPartiKodu.Substring(0, 3) == MvcApplication2.Models.Parti.BugununPartiKodu)
                {
                    partiSirasi = sonPartiKodu[3];
                    char biSonrakiPartiSirasi = (char)(partiSirasi + 1);

                    if (partiSirasi == '9')
                    {
                        partiSirasi = 'A';
                    }
                    else if (biSonrakiPartiSirasi == 'J')
                    {
                        partiSirasi = 'K';
                    }else if(biSonrakiPartiSirasi == 'Q')
                    {
                        partiSirasi = 'R';
                    }
                    else
                    {
                        partiSirasi = biSonrakiPartiSirasi;
                    }
                }
                return partiSirasi.ToString();
            }catch(Exception){
                return "PartiSirasiniAlamadim";
            }
        }        

        [ChildActionOnly]
        public int SiradakiIsEmriNo(){
            return db.Partis.Max(p => p.IsEmriNo) + 1;
        }

        //
        // GET: /Mil/Create

        public ActionResult Create()
        {
            return RedirectToAction("Duzenle");
        }

        public ActionResult Edit(int id = 0)
        {
            return RedirectToAction("Duzenle", new { id = id });
        }

        //edit yapiyor bu isi
        // GET: /Parti/Create 
        /*
        public ActionResult Create()
        {
            ViewBag.MilId = new SelectList(db.Mils.Include(m => m.Sanayi), "MilId", "AramaIsmi");

            return View(new Parti { 
                MilAdedi = null,
                PartiKodu = MvcApplication2.Models.Parti.BugununPartiKodu + SiradakiPartiSirasi(),
                IsEmriNo = SiradakiIsEmriNo(),
                GirisTarihi = DateTime.Today });
        }

        //
        // POST: /Parti/Create
        [HttpPost]
        public ActionResult Create(Parti parti)
        {
            if(db.Partis.Count(p => p.PartiKodu == parti.PartiKodu && p.PartiId != parti.PartiId) > 0)
                ModelState.AddModelError("PartiKodu", "Bu parti kodu zaten ekli");
            if (db.Partis.Count(p => p.IsEmriNo == parti.IsEmriNo && p.PartiId != parti.PartiId) > 0)
                ModelState.AddModelError("IsEmriNo", "Bu is emri no zaten var");
            
            if (ModelState.IsValid)
            {
                parti.GirisTarihi = DateTime.Now;
                db.Partis.Add(parti);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.MilId = new SelectList(db.Mils.Include(m => m.Sanayi), "MilId", "AramaIsmi",parti.MilId);
            return View(parti);
        }
        */

        //
        // GET: /Parti/Edit/5

        public ActionResult Duzenle(int id = 0)
        {
            Parti parti;
            if (id == 0)//yeni olustur
                parti = new Parti
                {
                    MilAdedi = null,
                    PartiKodu = MvcApplication2.Models.Parti.BugununPartiKodu + SiradakiPartiSirasi(),
                    IsEmriNo = SiradakiIsEmriNo(),
                    GirisTarihi = DateTime.Today
                };
            else
                parti = Partis.Single(p => p.PartiId == id);
            if (parti == null)
            {
                return HttpNotFound();
            }
            ViewBag.MilId = new SelectList(Mils, "MilId", "AramaIsmi", parti.MilId);
            return View(parti);
        }

        //
        // POST: /Parti/Edit/5
        [HttpPost]
        public ActionResult Duzenle(Parti parti)
        {
            if (db.Partis.Count(p => p.PartiKodu == parti.PartiKodu && p.PartiId != parti.PartiId) > 0)
                ModelState.AddModelError("PartiKodu", "Bu parti kodu zaten ekli");
            if (db.Partis.Count(p => p.IsEmriNo == parti.IsEmriNo && p.PartiId != parti.PartiId) > 0)
                ModelState.AddModelError("IsEmriNo", "Bu is emri no zaten var");

            if (ModelState.IsValid)
            {
                if (parti.PartiId == 0)
                    db.Partis.Add(parti);
                else
                    db.Entry(parti).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MilId = new SelectList(Mils, "MilId", "AramaIsmi", parti.MilId);
            return View(parti);
        }

        //
        // GET: /Parti/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Parti parti = db.Partis.Find(id);
            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }

        //
        // POST: /Parti/Delete/5

        [HttpPost, ActionName("Delete")]
        public string DeleteConfirmed(int id)
        {/*
            Parti parti = db.Partis.Find(id);
            db.Partis.Remove(parti);
            db.SaveChanges();
            return RedirectToAction("Index");
          * */
            return "henuz hazir degil";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}