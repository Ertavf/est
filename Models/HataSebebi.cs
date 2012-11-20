using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class HataSebebi
	{
		public int HataSebebiId {get; set;}
		[Required]
		public string Sebep {get; set;}
	}
}