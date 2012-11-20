using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MvcApplication2.Models
{
    [NotMapped]
    //[DataContract]
    public class Yedek
    {
        public List<Sanayi> Sanayiler { get; set; }

        public List<MilTipi> MilTipleri { get; set; }

        public List<Mil> Miller { get; set; }

        public List<Parti> Partiler { get; set; }

        public List<Personel> Personeller { get; set; }
        /*
        public DbSet<Tezgah> Tezgahs { get; set; }

        public DbSet<HataSebebi> HataSebebis { get; set; }

        public DbSet<HataliBolum> HataliBolums { get; set; }

        public DbSet<HataYakalayanBolum> HataYakalayanBolums { get; set; }

        public DbSet<HataliOperasyon> HataliOperasyons { get; set; }

        public DbSet<Hata> Hatas { get; set; }

        public DbSet<Bolum> Bolums { get; set; }
        */

    }
        
}