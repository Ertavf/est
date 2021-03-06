﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace MvcApplication2.Models.XmlVeriler
{
    class SiraNoComparer : IComparer<Parti>
    {
        int IComparer<Parti>.Compare(Parti x, Parti y)
        {
            return x.IsEmriNo - y.IsEmriNo;
        }
    }

    public class XmlOkuyucu
    {
        private string getValue(XElement element, params string[] values)
        {
            return element != null ? element.Value :
                ((values.Length > 0) ? values[0] : "?");
        }

        private string XmlDir = AppDomain.CurrentDomain.BaseDirectory + @"Models/XmlVeriler/";
        private string SeedLogDir = AppDomain.CurrentDomain.BaseDirectory + @"Models/XmlVeriler/SeedLoglari/";
#if DEBUG
        private const string StokXmlFile = "stok_azaltilmis.xml";
#else
        private const string StokXmlFile = "stok.xml";
#endif
        private const string ErkekXmlFile = "erkek.xml";
        private const string UretimCesitleriXml = "Uretimcesitleri.xml";
        private const string EstKodlariXmlFile = "estKodlari.xml";

        private System.IO.StreamWriter seedLogWriter;

        public XmlOkuyucu()
        {
            seedLogWriter = new System.IO.StreamWriter(SeedLogDir + "log" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".log");
        }
        public List<Parti> partiler(List<Sanayi> sanayis, List<Mil> mils, List<MilTipi> milTipis, List<EstasKoduTipi> estasKoduTipis, List<Firma> firmas, List<TaniticiRenk> taniticiRenks)
        {
            List<Parti> partis = new List<Parti>();

            XDocument stokXDocument =XDocument.Load(XmlDir + StokXmlFile);
            IEnumerable<XNode> stokIEnumerable = stokXDocument.Root.Nodes();

            XDocument erkekXDocument = XDocument.Load(XmlDir + ErkekXmlFile);
            IEnumerable<XNode> erkekIEnumerable = erkekXDocument.Root.Nodes();

            XDocument uretimCestileriXDocument = XDocument.Load(XmlDir + UretimCesitleriXml);
            IEnumerable<XNode> uretimCesitleriIEnumerable = uretimCestileriXDocument.Root.Nodes();

            XDocument estKodlariXDocument = XDocument.Load(XmlDir + EstKodlariXmlFile);
            IEnumerable<XNode> estKodlariIEnumerable = estKodlariXDocument.Root.Nodes();

            int siraNo = 0;
            foreach (XElement stokXElement in stokIEnumerable)
            {
                siraNo++; //
                int miktar;
                try
                {
                    miktar = int.Parse(getValue(stokXElement.Element("miktar")));
                }
                catch
                {
                    System.Diagnostics.Trace.TraceInformation("Partileri aktarirken miktar alamadim> Parti {0}", siraNo);
                    seedLogWriter.WriteLine("Partileri aktarirken miktar alamadim> Sira No: {0} Stok: {1}", siraNo, stokXElement.ToString());
                    continue;
                }

                string EstasKod = getValue(stokXElement.Element("kulamac"));

                if (EstasKod.Length < 8)
                {
                    System.Diagnostics.Trace.TraceInformation("Partileri aktarirken estas kodu alamadim> Parti {0}", siraNo);
                    seedLogWriter.WriteLine("Partileri aktarirken estas kodu alamadim> Sira No: {0} Stok: {1}", siraNo, stokXElement.ToString());
                    continue;
                }
                string estasKoduTipiStr = EstasKod.Substring(0, 3);
                string estasKodu_firma = EstasKod.Substring(4, 2);
                string estasKodu_FirmaninMilKodu = EstasKod.Substring(7, EstasKod.Length - 7);

                int estasKodu_firmaInt;
                int estasKodu_FirmaninMilKoduInt;

                IEnumerable<EstasKoduTipi> mevcutEstasKoduTipi =  estasKoduTipis.Where(e => e.EstasKoduTipiStr == estasKoduTipiStr);

                EstasKoduTipi estasKoduTipi;
                if (mevcutEstasKoduTipi.Count() == 0)
                {
                    estasKoduTipi = new EstasKoduTipi
                    {
                        EstasKoduTipiStr = estasKoduTipiStr,
                    };
                    estasKoduTipis.Add(estasKoduTipi);
                }
                else
                {
                    estasKoduTipi = estasKoduTipis.First();
                }

                if(!(int.TryParse(estasKodu_firma,out estasKodu_firmaInt) && (int.TryParse(estasKodu_FirmaninMilKodu,out estasKodu_FirmaninMilKoduInt)))){
                    System.Diagnostics.Trace.TraceInformation("Partileri aktarirken firma kodu alamadim> Parti {0}", siraNo);
                    seedLogWriter.WriteLine("Partileri aktarirken firma kodu alamadim> Sira No: {0} Stok: {1}", siraNo, stokXElement.ToString());
                    continue;
                }

                Firma firma;
                IEnumerable<Firma> mevcutFirmalar = firmas.Where(f => f.FirmaNumarasi == estasKodu_firmaInt);
                
                if (mevcutFirmalar.Count() == 0)
                {
                       IEnumerable<XNode> firmaKodluEst  =  estKodlariIEnumerable.Where(e => {
                           
                           string est_grubu = ((string)(((XElement)e).Element("EST_x0020_GRUBU")));
                           string firmaNumarasi = est_grubu.Split(new char[]{' ','.'},3 )[1];
                           int firmaNumarasiInt;

                           return int.TryParse(firmaNumarasi, out firmaNumarasiInt) && firmaNumarasiInt == estasKodu_firmaInt; 
                       });
                        //((string)(((XElement)e).Element("EST_x0020_GRUBU"))).Split(".",)[0] ==  "s" );
                    
                    string firmaIsmi = null;

                    
                    firmaKodluEst.ToList().ForEach(f =>
                    {
                        XElement firmaKodluEstElement = (XElement)(f);
                        if (firmaIsmi == null)
                            firmaIsmi = getValue(firmaKodluEstElement.Element("MİL_x0020_ADI"));
                        else
                            firmaIsmi += "-" + getValue(firmaKodluEstElement.Element("MİL_x0020_ADI")); ;
                    }
                    );
                    
                    if(firmaIsmi == null)
                    {
                        firmaIsmi = "Firma" + estasKodu_firma ;
                    }

                    firma = new Firma
                    {
                        FirmaIsmi = firmaIsmi,
                        FirmaNumarasi = estasKodu_firmaInt
                    };
                    firmas.Add(firma);
                }
                else
                {
                    firma = mevcutFirmalar.First();
                }

                //System.Diagnostics.Trace.TraceInformation("Firma {0}", firma);
                string milinEptsi = null;

                IEnumerable<XNode> EstasKodluEpt = erkekIEnumerable.Where(e => (string)((XElement)e).Element("_x0033_") == EstasKod && ((XElement)e).Element("_x0034_") != null);

                if (EstasKodluEpt.Count() == 0)
                {
                    string msg = string.Format("EPT alamadim> Sira No: {0} Erkek: {1}", siraNo, stokXElement.ToString());
                    System.Diagnostics.Trace.TraceInformation(msg);
                    seedLogWriter.WriteLine(msg);
                    continue;
                }

                XElement erkekEptElement = (XElement)EstasKodluEpt.First();

                milinEptsi = erkekEptElement.Element("_x0034_").Value;

                string dokumMil_ModelNo; //Eger milin model nosu varsa dokum mil var yoksa null
                
                IEnumerable<XNode> uretimCesitleriIEnumerableEstasKodlu = uretimCesitleriIEnumerable.Where(u =>
                    (string)((XElement)u).Element("ESTAÅ_x009E_KODU") == EstasKod && ((XElement)u).Element("DÃ_x2013_KÃœMMODELNO") != null);
                MilTipi milTipi;

                if (uretimCesitleriIEnumerableEstasKodlu.Count() == 0)
                {
                //    string msg = string.Format("Model no alamadim> Sira No: {0} Erkek: {1}", siraNo, stokXElement.ToString());
                //    System.Diagnostics.Trace.TraceInformation(msg);
                //    seedLogWriter.WriteLine(msg);
                    dokumMil_ModelNo = null;
                    milTipi = milTipis[1];
                }
                else
                {
                    XElement estasKodluModel = (XElement)uretimCesitleriIEnumerableEstasKodlu.First();
                    string modelNo = estasKodluModel.Element("DÃ_x2013_KÃœMMODELNO").Value;
                    dokumMil_ModelNo = modelNo ;
                    milTipi = milTipis[0];
                }

                Mil mil;
                IEnumerable<Mil> miller = mils.Where<Mil>(
                    m => m.EstasKodu_FirmaninMilKodu == estasKodu_FirmaninMilKoduInt &&
                        m.EstasKoduTipi == estasKoduTipi &&
                        m.Firma == firma);
                if (miller.Count() > 0)
                {
                    mil = miller.First();
                }
                else
                {
                    string renk = getValue(stokXElement.Element("birimfiyat"));
                    renk = (renk == "-") ? "Yok" : renk;

                    TaniticiRenk taniticiRenk;

                    IEnumerable<TaniticiRenk> mevcutTaniticiRenkler =  taniticiRenks.Where(t => t.TaniticiRenkStr == renk);

                    if (mevcutTaniticiRenkler.Count() == 0)
                    {
                        taniticiRenk = new TaniticiRenk
                        {
                            TaniticiRenkStr = renk,
                        };
                        taniticiRenks.Add(taniticiRenk);
                    }
                    else
                    {
                        taniticiRenk = mevcutTaniticiRenkler.First();
                    }

                    mil = new Mil
                    {
                        MilKodu = getValue(stokXElement.Element("stokadi")),
                        MilNo = getValue(stokXElement.Element("miktarbirim")),
                        MilAdi = getValue(stokXElement.Element("stokkodu")),
                        TaniticiRenk = taniticiRenk,
                        EstasKodu_FirmaninMilKodu = estasKodu_FirmaninMilKoduInt,
                        Aciklama = getValue(stokXElement.Element("username"),""),
                        Firma = firma,
                        Sanayi = Regex.IsMatch(EstasKod, "est", RegexOptions.IgnoreCase) ? sanayis[0] : sanayis[1],
                        Ept = milinEptsi,
                        DokumMil_ModelNo = dokumMil_ModelNo,
                        MilTipi = milTipi,
                        EstasKoduTipi = estasKoduTipi,
                    };
                    mils.Add(mil);
                };
                string PartiKod = getValue(stokXElement.Element("faturano"));
                string girisTarihString = getValue(stokXElement.Element("giristarihi"));

                DateTime girisTarih;
                try
                {
                    girisTarih = DateTime.ParseExact(girisTarihString, "yyyy-MM-dd'T'HH:mm:ss",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal);
                }
                catch
                {
                    girisTarih = DateTime.Now;
                }
                partis.Add
                (
                    new Parti
                    {
                        PartiId = siraNo,
                        IsEmriNo = siraNo,
                        Mil = mil,
                        PartiKodu = PartiKod,
                        MilAdedi = miktar,
                        GirisTarihi = girisTarih,
                        GirisKntRaporNo = getValue(stokXElement.Element("rafno"))
                    }
                );
            }

            partis.Sort(new SiraNoComparer());
            return partis;
        }
    }
}