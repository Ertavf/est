using System.ComponentModel.DataAnnotations;
using System;
namespace MvcApplication2.Models
{ 
	public class Hata
	{
		public int HataId {get; set;}

		public Parti Parti {get; set;}
        public int PartiId {get; set;}

        [Required]
        [DataType(DataType.Date)]
		public DateTime HataTarihi {get; set;}

        public int HataAdet {get; set;}

		public int HataSebebiId {get; set;}
		public HataSebebi HataSebebi {get; set;}
		
        public int HataliBolumId {get; set;}
		public HataliBolum HataliBolum {get; set;}
		
        public int HataYakalayanBolumId {get; set;}
		public HataYakalayanBolum HataYakalayanBolum {get; set;}
		
        public int OperatorId {get; set;}
		public Personel Operator {get; set;}
		
        public bool IadeEstas {get; set;}
        
        public Tezgah Tezgah { get; set; }
        public int TezgahId { get; set; }

        public int Tashih { get; set; }

	}
}