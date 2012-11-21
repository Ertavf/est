using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace MvcApplication2.Models
{
    class SeedData : DropCreateDatabaseIfModelChanges<MvcApplication2.Models.MvcApplication2Context>
    {
        protected override void Seed(MvcApplication2Context context)
        {
            base.Seed(context);
			
            List<Sanayi> sanayis = new List<Sanayi>
            {
                new Sanayi {SanayiTipi = "Ana"},
                new Sanayi {SanayiTipi = "Yan"}
            };
            sanayis.ForEach(s => context.Sanayis.Add(s));


            List<Tezgah> tezgahs = new List<Tezgah>{
                new Tezgah { TezgahAdi = "CNC Taşlama Tezgahı", TezgahKodu = "CF45"}
            };
            tezgahs.ForEach(t => context.Tezgahs.Add(t));

            List<MilTipi> milTipis = new List<MilTipi>{
                    new MilTipi{MilTipiId = 1, MilTipiIsmi = "Döküm"},
                    new MilTipi{MilTipiId = 2, MilTipiIsmi = "Çelik"}
                };
            milTipis.ForEach(m => context.MilTipis.Add(m));

            List<Mil> mils = new List<Mil>();

            List<EstasKoduTipi> estasKoduTipis = new List<EstasKoduTipi>();

            List<Firma> firmas = new List<Firma>();

            List<TaniticiRenk> taniticiRenks = new List<TaniticiRenk>();

            XmlVeriler.XmlOkuyucu xmlVeriler = new XmlVeriler.XmlOkuyucu();
            List<Parti> partis = xmlVeriler.partiler(sanayis, mils, milTipis, estasKoduTipis, firmas, taniticiRenks);
            partis.ForEach(p => context.Partis.Add(p));

            List<HataSebebi> hataSebepleri = new List<HataSebebi>{
                new HataSebebi{ Sebep = "Örnek Hata Sebebi"}
            };
            hataSebepleri.ForEach(h => context.HataSebebis.Add(h));

            List<HataliBolum> hataliBolumler = new List<HataliBolum>{
                new HataliBolum{ BolumIsmi = "Örnek Hatalı Bölüm"}
            };
            hataliBolumler.ForEach(h => context.HataliBolums.Add(h));

            List<HataYakalayanBolum> hataYakalayanBolumler = new List<HataYakalayanBolum>
            {
                 new HataYakalayanBolum{ BolumIsmi = "Örnek Hata Yakalayan Bölüm"}
            };
            hataYakalayanBolumler.ForEach(h => context.HataYakalayanBolums.Add(h));

            List<HataliOperasyon> hataliOperasyonlar = new List<HataliOperasyon>
            {
                new HataliOperasyon{ OperasyonAdi = "Örnek Hatalı Operasyon"}
            };
            hataliOperasyonlar.ForEach(h => context.HataliOperasyons.Add(h));

            List<Personel> operatorler = new List<Personel>();

            List<Bolum> bolumler = new List<Bolum>();

            Models.XmlVeriler.OperatorSeeder operatorSeeder = new Models.XmlVeriler.OperatorSeeder();

            operatorSeeder.seed(operatorler,bolumler);

            operatorler.ForEach(h => context.Personels.Add(h));
            //bolumler.ForEachh otomatik

            List<Hata> hatalar = new List<Hata>
            {
                new Hata{
                    HataAdet = 1, HataliBolum = hataliBolumler[0], HataSebebi = hataSebepleri[0], 
                    HataTarihi = new System.DateTime(2012,1,1,1,1,1),
                    HataYakalayanBolum = hataYakalayanBolumler[0],
                    IadeEstas = false, Operator = operatorler[0], Parti = partis[0], Tashih = 1, Tezgah = tezgahs[0]
                }
            };
            hatalar.ForEach(h => context.Hatas.Add(h));

            try
            {
                //IEnumerable verr = context.GetValidationErrors();
                
                //System.Diagnostics.Trace.WriteLine(verr.ToString());

                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        
    }
}
