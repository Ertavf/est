using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class Personel
	{

        public int PersonelId { get; set; }
		[Required]
		public string SicilNo {get; set;}
		[Required]
		public string Adi {get; set;}
		[Required]
		public string Soyadi {get; set;}
		
        //public System.Web.UI.WebControls.Image Resim { get; set; }
        public Bolum Bolum { get; set; }
        public int BolumId { get; set; }

        [Required]
        public string ResimLink { get; set; }
	}
}