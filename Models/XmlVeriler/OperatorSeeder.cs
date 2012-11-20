using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MvcApplication2.Models.XmlVeriler
{


    public class OperatorSeeder
    {
        private string XmlDir = AppDomain.CurrentDomain.BaseDirectory + @"Models/XmlVeriler/";
        private string SeedLogDir = AppDomain.CurrentDomain.BaseDirectory + @"Models/XmlVeriler/SeedLoglari/";
        private const string PersonelXmlFile = "PERSONEL.xml";

        internal void seed(List<Personel> operatorler, List<Bolum> bolumler)
        {
            XDocument personelXDocument = XDocument.Load(XmlDir + PersonelXmlFile);
            IEnumerable<XNode> personelEnumerable = personelXDocument.Root.Nodes();

            foreach (XElement personelXElement in personelEnumerable)
            {
                string bolumStr = getValue(personelXElement.Element("BÖLÜM"));
                IEnumerable<Bolum> bulunanBolumler = bolumler.Where(b => b.BolumAdi.Equals(bolumStr, StringComparison.CurrentCultureIgnoreCase));

                Bolum bulunanBolum;
                if (bulunanBolumler.Count() > 0)
                {
                    bulunanBolum = bulunanBolumler.First();
                }
                else
                {
                    bulunanBolum = new Bolum
                    {
                        BolumAdi = bolumStr
                    };
                    bolumler.Add(bulunanBolum);
                }
                string sicilNo = getValue(personelXElement.Element("SİCİLNO"));
                operatorler.Add(new Personel
                {
                    SicilNo = sicilNo,
                    Adi = getValue(personelXElement.Element("İSİM")),
                    Soyadi = getValue(personelXElement.Element("SOYISIM")),
                    Bolum = bulunanBolum,
                    ResimLink = "/Images/Personel/"+sicilNo + ".jpg"
                });


            }

        }

        private string getValue(XElement element, params string[] values)
        {
            return element != null ? element.Value :
               ((values.Length > 0) ? values[0] : "?");
        }
    }
}