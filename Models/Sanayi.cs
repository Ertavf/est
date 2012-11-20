using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
namespace MvcApplication2.Models
{ 
	public class Sanayi
	{
		public int SanayiId {get; set;}
		[Required]
		public string SanayiTipi {get; set;}

        public ICollection<Mil> Mils { get; set; }

	}
}