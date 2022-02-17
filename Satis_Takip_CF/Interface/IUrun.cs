using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Satis_Takip_CF.Interface
{
    interface IUrun
    {
         SqlConnection  Baglanti { get ;  }

         DataTable UrunGetir();

        void UrunOlustur(Tablolar.Urun _Urun );

        void UrunSil(Tablolar.Urun _Urun);

        void UrunGuncelle(Tablolar.Urun _Urun);

        DataTable UrunGetirSec(byte _UrunTur );
        
    }
}
