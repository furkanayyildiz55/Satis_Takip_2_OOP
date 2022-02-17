using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Satis_Takip_CF.Interface;
using System.Data;
using System.Data.SqlClient;
using Satis_Takip_CF.VeritabaniIslemleri;
using Satis_Takip_CF.Tablolar;

namespace Satis_Takip_CF.VeritabaniIslemleri
{
    public class UrunIslemler : IUrun
    {
        private SqlConnection _Baglanti = VeriTabani.Baglanti();
        public SqlConnection Baglanti
        {
            get
            {
             return _Baglanti;
            }
        }

        public DataTable UrunGetir()
        {
            DataTable DT;
            SqlDataAdapter DA;
            
            DA = new SqlDataAdapter("select * from urun", Baglanti);
            DT = new DataTable();
            Baglanti.Open();
            DA.Fill(DT);
            Baglanti.Close();
            return DT;
        }

        public void UrunOlustur (Tablolar.Urun  _Urun)
        {
            SqlCommand UrunOlustur = new SqlCommand("insert into Urun (UrunAd,UrunAdet,UrunFiyat,UrunTur) values (@p1,@p2,@p3,@p4)", Baglanti);
            UrunOlustur.Parameters.AddWithValue("@p1", _Urun.UrunAd);  //parametrelere toollardan textler aktarılır
            UrunOlustur.Parameters.AddWithValue("@p2", _Urun.UrunAdet.ToString());
            UrunOlustur.Parameters.AddWithValue("@p3", _Urun.UrunFiyat.ToString());
            UrunOlustur.Parameters.AddWithValue("@p4", _Urun.UrunTur);
            Baglanti.Open();
            UrunOlustur.ExecuteNonQuery();
            _Baglanti.Close();
        }

        public void UrunSil(Tablolar.Urun _Urun)
        {
            SqlCommand UrunSil = new SqlCommand("Delete from Urun where  UrunID=@p1", Baglanti);
            UrunSil.Parameters.AddWithValue("@p1", _Urun.UrunID.ToString());
            Baglanti.Open();
            UrunSil.ExecuteNonQuery();
            Baglanti.Close();
        }

        public void UrunGuncelle(Tablolar.Urun _Urun)
        {
            SqlCommand UrunGuncelle = new SqlCommand("update Urun set Urunad=@p1,UrunAdet=@p2 ,UrunFiyat=@p3, uruntur=@p4 where urunID=@p5", Baglanti);
            UrunGuncelle.Parameters.AddWithValue("@p1", _Urun.UrunAd);
            UrunGuncelle.Parameters.AddWithValue("@p2", _Urun.UrunAdet.ToString());
            UrunGuncelle.Parameters.AddWithValue("@p3", _Urun.UrunFiyat.ToString());
            UrunGuncelle.Parameters.AddWithValue("@p4", _Urun.UrunTur);
            UrunGuncelle.Parameters.AddWithValue("@p5", _Urun.UrunID.ToString());


            Baglanti.Open();
            UrunGuncelle.ExecuteNonQuery();
            Baglanti.Close();
        }

        public DataTable UrunGetirSec(byte _UrunTur)
        {

            string komut="";

            if (_UrunTur==1)
                komut = "select * from urun where uruntur='true'";
            else if (_UrunTur==2)
                komut = "select * from urun where uruntur='false'";
            else if(_UrunTur==0)
                komut = "select * from urun ";

            SqlDataAdapter DA = new SqlDataAdapter(komut, Baglanti);
            DataTable DT = new DataTable();
            Baglanti.Open();
            DA.Fill(DT);
            Baglanti.Close();
            return DT;
        }


    }
}
