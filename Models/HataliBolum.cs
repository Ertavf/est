using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class HataliBolum
	{
		public int HataliBolumId {get; set;}
		[Required]
		public string BolumIsmi {get; set;}
	}
}