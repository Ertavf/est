using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization; 
namespace MvcApplication2.Models
{
    [DataContract]
    public class Mil
	{
        [DataMember]
		public int MilId {get; set;}
        
		public Sanayi Sanayi {get; set;}
        [DataMember]
        public int SanayiId { get; set; }
        
        [DataMember]
        [Required]
        public string MilAdi { get; set; }


        public string EstasKodu
        {
            get
            {
                if (EstasKoduTipi != null && Firma != null)
                    return string.Format("{0} {1:00}.{2:000}", EstasKoduTipi.EstasKoduTipiStr,Firma.FirmaNumarasi,EstasKodu_FirmaninMilKodu);
                        //EstasKoduTipi.EstasKoduTipiStr + " " + Firma.FirmaEstKodu + "." + EstasKodu_FirmaninMilKodu;
                else
                    return "EstasKoduTipi ya da Firma null";
            }
        }
        

        public EstasKoduTipi EstasKoduTipi { get; set; }
        [DataMember]
        public int EstasKoduTipiId { get; set; }

        [Required]
        [DataMember]
        public int FirmaId { get; set; }
        public Firma Firma { get; set; }

        [Required]
        [DataMember]
        public int? EstasKodu_FirmaninMilKodu { get; set; }

        [Required]
        //[RegularExpression(@"\d\d.\d\d.\d\d",ErrorMessage="Hatali")]
        [DataMember]
        public string MilNo { get; set; }
        
        [ScaffoldColumn(false)]
        [Display(Name = "Mil(Sanayi - Estaþ Kodu - Mil Kodu - Mil Adý -  Mil No - Tanýtýcý Renk - (Mil Tipi - Model No) - EPT )")]
        public string AramaIsmi {
            get
            {
                return (Sanayi!= null? Sanayi.SanayiTipi + "\t- ": "")
                       + EstasKodu + "\t- " + MilKodu + "\t- " + MilAdi + "\t- " + MilNo + "\t- " + TaniticiRenk + "\t- "+
                       MilTipiStr + "\t- " + Ept; 
            } 
        }

        [Required]
        [DataMember]
        public string MilKodu { get; set; }


        //public string TaniticiRenk { get; set; }

        public TaniticiRenk TaniticiRenk { get; set; }
        [Required]
        [DataMember]
        public int TaniticiRenkId { get; set; }

        [Required]
        [DataMember]
        public string Aciklama { get; set; }
        
        [Required]
        [DataMember]
        public string Ept { get; set; }

        [Display(Name = "Mil Tipi - Model No")]
        public string MilTipiStr { 
            get {
                return (MilTipi != null) ? MilTipi.MilTipiIsmi + (MilTipi.MilTipiId == 1 ? " " + DokumMil_AramaIsmi : "") : "MilTipiNull";
            } // 
        }

        public MilTipi MilTipi {get; set;}

        [DataMember]
        public int MilTipiId {get;set;}

        /*
            {
                return DokumMil != null ? DokumMil.AramaIsmi : "Çelik";
            }*/
        public string DokumMil_AramaIsmi
        {
            get
            {
                return (DokumMil_ModelNo != null) ? DokumMil_ModelNo : "DokumMilNull";
            }
        }
        [DataMember]
        public string DokumMil_ModelNo { get; set; }

        public ICollection<Parti> MilPartis { get; set;}
    }
}