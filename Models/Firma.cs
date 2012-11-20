using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcApplication2.Models
{
    public class Firma
    {
        public int FirmaId { get; set; }

        [Required]
        public string FirmaIsmi { get; set; }

        public int FirmaNumarasi { get; set; }

        public string FirmaAramaStr
        {
            get
            {
                return FirmaNumarasi.ToString("00") + " " + FirmaIsmi;
            }
        }
    }
}
