using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class HataYakalayanBolum
	{
		public int HataYakalayanBolumId {get; set;}
		[Required]
		public string BolumIsmi {get; set;}
	}
}