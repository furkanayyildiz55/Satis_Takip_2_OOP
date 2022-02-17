using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Satis_Takip_CF.AbstractSınıf;
using System.Collections;

namespace Satis_Takip_CF.VeritabaniIslemleri
{
    
    public class SatisSonrasıIslemler :AbsBaglanti
    {
        private SqlConnection _Baglanti = VeriTabani.Baglanti();
        public override SqlConnection Baglanti
        {
            get
            {
                return _Baglanti;
            }
        }

        public DataTable SatisGetir()
        {
            SqlDataAdapter DA = new SqlDataAdapter("select * from satis", Baglanti);
            DataTable DT = new DataTable();
            Baglanti.Open();
            DA.Fill(DT);
            Baglanti.Close();
            return DT;
        }

        public void SatisSil(string _SatisID )
        {
            SqlCommand Satis = new SqlCommand("delete from satis where satısID=@p1", Baglanti);
            Satis.Parameters.AddWithValue("@p1", _SatisID.ToString());
            Baglanti.Open();
            Satis.ExecuteNonQuery();
            Baglanti.Close();
        }
    }


    public class SatisOlusturİslemler :AbsBaglanti
    {
        private SqlConnection _Baglanti = VeriTabani.Baglanti();
        public override SqlConnection Baglanti
        {
            get
            {
                return _Baglanti;
            }
        }

        public ArrayList MusteriIsimGetir()
        {
            ArrayList _MusteriIsim = new ArrayList();

            SqlCommand CMBmusteriEkle = new SqlCommand("select MusteriAd from musteri ", Baglanti);
            Baglanti.Open();
            SqlDataReader dr33 = CMBmusteriEkle.ExecuteReader();
            while (dr33.Read())
            {
                _MusteriIsim.Add(dr33[0].ToString());   //her müşteri _MusteriIsim a item olarak ekleniyor
            }
            Baglanti.Close();

            return _MusteriIsim;
        }

        public ArrayList UrunIsimGetir(int _Secim )
        {
            ArrayList _UrunIsim = new ArrayList();
            string Komut="";

            switch (_Secim)
            {
                case 0:
                    Komut = "select urunad from urun where  UrunAdet>0 ";
                    break;
                case 1:
                    Komut = "select urunad from urun where uruntur='True' and UrunAdet>0 ";
                    break;
                case 2:
                    Komut = "select urunad from urun where uruntur='False' and UrunAdet>0 ";
                    break;
            }

            SqlCommand UrunIsimCek = new SqlCommand(Komut, Baglanti);
            Baglanti.Open();
            SqlDataReader DR = UrunIsimCek.ExecuteReader();

            while (DR.Read())
            {
                _UrunIsim.Add(DR[0].ToString());  
            }
            Baglanti.Close();

            return _UrunIsim;

        }


        public string UrunFiyatCek(string _UrunIsim)
        {
            string UrunFiyat = "";
           
            SqlCommand UrunFiyatCek = new SqlCommand("select urunfiyat from Urun where UrunAd=@p1 ", Baglanti);
            UrunFiyatCek.Parameters.AddWithValue("@p1", _UrunIsim);
            Baglanti.Open();
            SqlDataReader FiyatOku = UrunFiyatCek.ExecuteReader();
            while (FiyatOku.Read())
            {
                UrunFiyat = FiyatOku[0].ToString();
            }
            Baglanti.Close();

            return UrunFiyat;
        }

        public void SatisOlustur(Tablolar.Satis _Satis )
        {

            SqlCommand FisOlustur = new SqlCommand("insert into satis (Musteri,Tarih,Urun,Adet,Fiyat,Gtoplam) values (@p1,@p2,@p3,@p4,@p5,@p6)", Baglanti); //tbl_satıs tablosuna ekleme yapar
            FisOlustur.Parameters.AddWithValue("@p1", _Satis.Musteri); //parametreler ilgli değişkenden alınır 
            FisOlustur.Parameters.AddWithValue("@p2", _Satis.Tarih);
            FisOlustur.Parameters.AddWithValue("@p3", _Satis.Urun);
            FisOlustur.Parameters.AddWithValue("@p4", _Satis.Adet);
            FisOlustur.Parameters.AddWithValue("@p5", _Satis.Fiyat);
            FisOlustur.Parameters.AddWithValue("@p6", _Satis.GToplam);
            Baglanti.Open();
            FisOlustur.ExecuteNonQuery();
            Baglanti.Close();

        }






    }
}
