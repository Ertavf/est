using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MvcApplication2.Models
{
    public class MvcApplication2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // 

        public DbSet<Sanayi> Sanayis { get; set; }

        public DbSet<MilTipi> MilTipis { get; set; }

        public DbSet<EstasKoduTipi> EstasKoduTipis { get; set; }

        public DbSet<Firma> Firmas { get; set; }

        public DbSet<Mil> Mils { get; set; }

        public DbSet<Parti> Partis { get; set; }

        public DbSet<Tezgah> Tezgahs { get; set; }

        public DbSet<HataSebebi> HataSebebis { get; set; }

        public DbSet<HataliBolum> HataliBolums { get; set; }

        public DbSet<HataYakalayanBolum> HataYakalayanBolums { get; set; }

        public DbSet<HataliOperasyon> HataliOperasyons { get; set; }

        public DbSet<Hata> Hatas { get; set; }

        public DbSet<Bolum> Bolums { get; set; }

        public DbSet<Personel> Personels { get; set; }

    }
}