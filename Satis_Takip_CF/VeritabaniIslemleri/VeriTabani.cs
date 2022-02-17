using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;  //app.config den ConnectionStrings çekebilmek için
using Satis_Takip_CF.Contexts;




namespace Satis_Takip_CF.VeritabaniIslemleri
{
    public class VeriTabani
    {


        public static SqlConnection Baglanti()
        {
            //app.confing den SatisTakipContext adlı ConnectionStrings çekioruz
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SatisTakipContext"].ConnectionString);
            return con;

        }


        public static string VTOlustur()
        {
            try
            {
                SatisTakipContext satisTakip = new SatisTakipContext();
                satisTakip.Database.Create();
                return "Veritabanı Oluşturuldu";
            }
            catch (Exception)
            {
                return "Veritabnı Oluşturmada hata!";
            }
        }

        public static int VTKontrol()
        {
            try
            {
                string VeritataniAdi = "SATIS_TAKIP_1";  //Veritabanı adı app.config >connection string > Database 

                SqlConnection baglanti = new SqlConnection(@"server=.\SQLEXPRESS;database=Master; Integrated Security=SSPI");
                //SQL icerisindeki tüm veritabanı bilgileri MASTER tablosunda tutulur. Master Tablosunu sorgular
                SqlCommand komut = new SqlCommand("SELECT Count(name) FROM master.dbo.sysdatabases WHERE name=@prmVeritabani", baglanti);
                 
                komut.Parameters.AddWithValue("@prmVeriTabani", VeritataniAdi);
                baglanti.Open();
                //Executescalar ile sorgu sonucunda dönen degeri aliyoruz. Veritabanı varsa 1 yoksa 0 dönecektir.
                int sonuc = (int)komut.ExecuteScalar();
                baglanti.Close();

                return sonuc;
            }
            catch (Exception)
            {
                return 2 ;
            }
            
        }

    }
}
