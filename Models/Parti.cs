using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace MvcApplication2.Models
{
    [DataContract]
	public class Parti
	{
        //[UIHint("DropDown", "MVC")]
        [DataMember]
        public int MilId { get; set; }

        [ScaffoldColumn(false)]
        [DataMember]
        public int PartiId { get; set; }
        
        [Required]
        [StringLength(maximumLength:4,MinimumLength=4,ErrorMessage="Parti Kodu 4 karakterden olusmali")]
        [DataMember]
        public string PartiKodu { get; set; }
        
        [Required]
        [Display(Name = "Ýþ Emri No")]
        [Range(1,int.MaxValue,ErrorMessage = "Is emri no 1den buyuk olmali")]
        [DataMember]
        public int IsEmriNo { get; set; }
		public Mil Mil {get; set;}

        [Required]
        [DataMember]
		public int? MilAdedi {get; set;}
        
        [Required]
        [DataMember]
        public string GirisKntRaporNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[UIHint("GirisTarihiEditlenebilirDegil")]
        [DataMember]
        public DateTime GirisTarihi { get; set; }

        [ScaffoldColumn(false)]
        //TODO
        public bool isValid { get; set; }

        public static string BugununPartiKodu
        {
            get
            {
                char ay = (char)((DateTime.Today.Month ) + 'A');
                char yil = (char)(DateTime.Today.Year - 2001 + 'A');
                char gun;
                if (DateTime.Today.Day < 10)
                    gun = (char)(DateTime.Today.Day + '0');
                else
                {
                    gun = (char)(DateTime.Today.Day - 10 + 'A');
                }
                if (DateTime.Today.Day > 18)
                {
                    gun++;
                }
                if (DateTime.Today.Day >= 25)
                {
                    gun++;
                }
                return ay + "" + yil + "" + gun;
            }
        }
        public ICollection<Hata> Hatas { get; set; }

    }
}