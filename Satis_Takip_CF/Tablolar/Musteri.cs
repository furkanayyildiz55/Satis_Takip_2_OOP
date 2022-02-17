using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  //Key Attribute için gerekli

namespace Satis_Takip_CF.Tablolar
{
    public class Musteri
    {
        [Key]
        public int MusteriID { get; set; }

        public string MusteriAd { get; set; }
        public string  MusteriTc { get; set; }
        public string  Musteriİl { get; set; }


    }
}
