using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class Tezgah
	{
		public int TezgahId {get; set;}
		[Required]
		public string TezgahKodu {get; set;}
		[Required]
		public string TezgahAdi {get; set;}
	}
}