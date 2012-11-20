using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcApplication2.Models
{
    public class DokumMil
    {
        public int DokumMilId { get; set; }
        [Required]
        public string ModelNo { get; set; }

        public string AramaIsmi {
            get{
                return ModelNo;
            }
        }
    }
}
