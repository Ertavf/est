using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MvcApplication2.Models
{
    [DataContract]
    public class MilTipi
    {
        [DataMember]
        public int MilTipiId {get;set;}
        [DataMember]
        public string MilTipiIsmi { get; set; }

        public ICollection<Mil> Mils { get; set; }

    }
}
