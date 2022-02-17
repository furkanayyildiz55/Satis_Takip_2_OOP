using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Satis_Takip_CF.VeritabaniIslemleri;
using Satis_Takip_CF.Tablolar;
using Satis_Takip_CF.AbstractSınıf;
using System.Collections;

namespace Satis_Takip_CF.VeritabaniIslemleri
{
    class MusteriIslemler : AbsBaglanti
    {
        private SqlConnection _Baglanti = VeriTabani.Baglanti();
      
        public override SqlConnection Baglanti
        {
            get
            {
                return _Baglanti;
            }
        }

        public DataTable MusteriGetir()
        {
            SqlDataAdapter DA = new SqlDataAdapter("select * from musteri", Baglanti);
            DataTable DT = new DataTable();
            Baglanti.Open();
            DA.Fill(DT);
            Baglanti.Close();
            return DT;
        }

        public void MusteriOlustur (Tablolar.Musteri _Musteri)
        {
            SqlCommand MusteriOlustur = new SqlCommand("insert into musteri (musteriAd,musteriTC,Musteriİl) values (@p1,@p2,@p3)", Baglanti);
            MusteriOlustur.Parameters.AddWithValue("@p1", _Musteri.MusteriAd);
            MusteriOlustur.Parameters.AddWithValue("@p2", _Musteri.MusteriTc);
            MusteriOlustur.Parameters.AddWithValue("@p3", _Musteri.Musteriİl);

            Baglanti.Open();
            MusteriOlustur.ExecuteNonQuery();
            Baglanti.Close();
        }

        public void MusteriSil (Tablolar.Musteri _Musteri)
        {
            SqlCommand MusteriSil = new SqlCommand("delete from musteri where MusteriID=@p1", Baglanti);
            MusteriSil.Parameters.AddWithValue("@p1", _Musteri.MusteriID.ToString());
            Baglanti.Open();
            MusteriSil.ExecuteNonQuery();
            Baglanti.Close();
        }

        public void MusteriGuncelle (Tablolar.Musteri _Musteri)
        {
           
            SqlCommand MusteriGuncelle = new SqlCommand("update musteri set musteriad=@p1,musteriTC=@p2 ,musteriİl=@p4 where musteriID=@p3", Baglanti);
            MusteriGuncelle.Parameters.AddWithValue("@p1", _Musteri.MusteriAd);
            MusteriGuncelle.Parameters.AddWithValue("@p3", _Musteri.MusteriID);
            MusteriGuncelle.Parameters.AddWithValue("@p2", _Musteri.MusteriTc);
            MusteriGuncelle.Parameters.AddWithValue("@p4", _Musteri.Musteriİl);


            Baglanti.Open();
            MusteriGuncelle.ExecuteNonQuery();
            Baglanti.Close();
        }

        public ArrayList MusteriGetirSec()
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

        public DataTable MusteriGetirIsmeGore(string _MusteriAd )
        {
            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter("select * from Satis where musteri=@p1  ", Baglanti); //alınan parametre ile şart koşarak veritabanından ilgili müsterinin yaptığı alışveriş dgv ye dolacak
            DA.SelectCommand.Parameters.AddWithValue("@p1", _MusteriAd) ;
            DA.Fill(DT);

            return DT;
        }



    }
}
