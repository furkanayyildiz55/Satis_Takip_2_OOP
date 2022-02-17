using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Satis_Takip_CF.AbstractSınıf
{
    public abstract class AbsBaglanti
    {
       abstract public SqlConnection Baglanti { get; }
    }
}
