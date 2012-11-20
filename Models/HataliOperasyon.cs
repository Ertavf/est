using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class HataliOperasyon
	{
		public int HataliOperasyonId {get; set;}
		[Required]
		public string OperasyonAdi {get; set;}
	}
}