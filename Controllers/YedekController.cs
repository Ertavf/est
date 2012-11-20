using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MvcApplication2.Controllers
{
    public class YedekController : Controller
    {
        private MvcApplication2Context db = new MvcApplication2Context();
        private string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        private string xmlPath = @"ydk/XmlVeriler/" + DateTime.Now.ToString("yyyy-MM-ddTHHmmss")+".xml";
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            Yedek yedek = new Yedek
            {
                Sanayiler = db.Sanayis.ToList(),
                MilTipleri = db.MilTipis.ToList(),
                Miller = db.Mils.ToList(),
                Partiler = db.Partis.ToList(),
                Personeller = db.Personels.ToList()
            };
            DataContractSerializer ds = new DataContractSerializer(typeof(Yedek));
            
            var settings = new XmlWriterSettings { Indent = true };

            var w = XmlWriter.Create(baseDir + xmlPath, settings);

            ds.WriteObject(w, yedek);
            
            w.Close();

            return Redirect(xmlPath);
        }

    }
}
