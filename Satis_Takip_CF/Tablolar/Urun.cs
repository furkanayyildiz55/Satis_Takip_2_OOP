using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  //Key Attribute için gerekli


namespace Satis_Takip_CF.Tablolar
{
    public class Urun
    {
        public int UrunID  { get; set; }

        public string UrunAd { get; set; }
        public int UrunAdet { get; set; }
        public int UrunFiyat { get; set; }
        public bool UrunTur { get; set; }

    }
}
