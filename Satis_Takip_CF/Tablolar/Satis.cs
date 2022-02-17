using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  //Key Attribute için gerekli


namespace Satis_Takip_CF.Tablolar
{
    public class Satis
    {
        [Key]
        public int SatısID { get; set; }

        public string Musteri { get; set; }
        public DateTime Tarih { get; set; }
        public string Urun { get; set; }
        public int Adet { get; set; }
        public int Fiyat { get; set; }
        public int GToplam { get; set; }
    }
}
