using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satis_Takip_CF.İndirim
{
    public class OranAsımHatası :Exception
    {
        public OranAsımHatası(string Mesaj) : base(Mesaj)   //eğer public tanımlamazsak varsayılan private olur ve throw içinde erişlemez.
        {
            
        }

        [Obsolete("Bu kullanım iptal edilmiştir",false) ]
        public OranAsımHatası() : base("Oran Değeri 100 ü Geçemez")   //şeklinde bir tanımlama da yapılabilir
        {

        }

    }
}
