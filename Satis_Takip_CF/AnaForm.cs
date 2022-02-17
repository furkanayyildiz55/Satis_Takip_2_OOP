using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Satis_Takip_CF.VeritabaniIslemleri;
using Satis_Takip_CF.Tablolar;
using Satis_Takip_CF.İndirim;

namespace Satis_Takip_CF
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        SatisOlusturİslemler SatisOlusturIslemler = new SatisOlusturİslemler();
        UrunIslemler UrunIslemler = new UrunIslemler();
        MusteriIslemler MusteriIslemler = new MusteriIslemler();
        SatisSonrasıIslemler SatisSonrasıIslemler = new SatisSonrasıIslemler();



        private void AnaForm_Load(object sender, EventArgs e)
        {
            DGV3UrunYenile();
            DGV5MusteriYenile();
            DGV6SatisDoldur();
            Cmb5MusteriIlDoldur();

        }



        #region SAYFA1_SATIŞ_OLUŞTUR

        private void cmb1Musteri_Click(object sender, EventArgs e)
        {
            try
            {
                cmb1Musteri.Items.Clear();
                foreach (var item in SatisOlusturIslemler.MusteriIsimGetir())
                {
                    cmb1Musteri.Items.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("1. Sayfada Musteri Eklemede Hata");
            }
        }

        private void cmb1Ürün_Click(object sender, EventArgs e)
        {
            try
            {
                int Secim;

                if (rd1Mobilya.Checked)
                    Secim = 1;
                else if (rd1Koltuk.Checked)
                    Secim = 2;
                else
                    Secim = 0;

                cmb1Ürün.Items.Clear();
                foreach (var item in SatisOlusturIslemler.UrunIsimGetir(Secim))
                {
                    cmb1Ürün.Items.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("1. Sayfada Ürün Eklemede Hata");
            }
        }

        private void cmb1Ürün_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb1Ürün.Text != "")
                {
                    Txt1ÜrünFiyat.Text = SatisOlusturIslemler.UrunFiyatCek(cmb1Ürün.Text);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("1. Sayfada Ürün Fiyat Eklemede Hata");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Txt1ÜrünFiyat.Text != "")
            {
                Txt1GenelToplam.Text = (Convert.ToInt32(Txt1ÜrünFiyat.Text) * (numericUpDown1.Value)).ToString();
            }
            else
            {
                numericUpDown1.Value = 0;
            }
        }

        private void Btn1SatısOlustur_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmb1Musteri.Text != "" && cmb1Ürün.Text != "" && Txt1ÜrünFiyat.Text != "" && Txt1GenelToplam.Text != "" && numericUpDown1.Value != 0)
                {
                    Satis _Satis = new Satis();
                    _Satis.Urun = cmb1Ürün.Text;
                    _Satis.Musteri = cmb1Musteri.Text;
                    _Satis.Tarih = dateTimePicker1.Value;
                    _Satis.Fiyat = Convert.ToInt32(Txt1ÜrünFiyat.Text);
                    _Satis.Adet = Convert.ToInt32(numericUpDown1.Value);
                    _Satis.GToplam = Convert.ToInt32(Txt1GenelToplam.Text);

                    SatisOlusturIslemler.SatisOlustur(_Satis);

                    MessageBox.Show("Satış Oluşturma İşlemi Başarılı");

                }
                else
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurun");
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Satış Oluşturma İşlemi Başarısız");
            }
            finally
            {
                cmb1Musteri.SelectedItem = null;
                cmb1Ürün.SelectedItem = null;
                Txt1ÜrünFiyat.Text = "";
                numericUpDown1.Value = 0;
                Txt1GenelToplam.Text = "";
                rd1Koltuk.Checked = false;
                rd1Mobilya.Checked = false;
                rd1FiyatOdaklı.Checked = false;
                rd1OgrenciInd.Checked = false;
                rd1YuzdeOdaklı.Checked = false;
                Txt1IndirimDegeri.Text = "";

            }

        }




        #endregion

        #region SAYFA1_ÜRÜN_OLUŞTUR_İNDİRİM_KISMI

        private void BtnIndirimUygula_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt1ÜrünFiyat.Text != "")
                {
                    int UrunFiyat = Convert.ToInt32(Txt1ÜrünFiyat.Text);
                    Indirim indirim;

                    if (rd1OgrenciInd.Checked)
                    {
                        indirim = new Indirim(UrunFiyat);
                        Txt1ÜrünFiyat.Text = indirim.OgrenciIndirimHesapla().ToString();
                    }
                    else if (rd1YuzdeOdaklı.Checked)
                    {
                        indirim = new Indirim(UrunFiyat, Convert.ToInt32(Txt1IndirimDegeri.Text));
                        Txt1ÜrünFiyat.Text = indirim.YuzdeOdakliİndirim().ToString();
                    }
                    else if (rd1FiyatOdaklı.Checked)
                    {
                        indirim = new Indirim(UrunFiyat, Convert.ToInt32(Txt1IndirimDegeri.Text), false);
                        Txt1ÜrünFiyat.Text = indirim.FiyatOdakliIndirim().ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Ürün Seçiniz");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İndirim gerçekleştirilemedi \n\n Hata Mesajı : " + ex.Message);
                Txt1IndirimDegeri.Text = "";
            }

        }
        #endregion


        #region SAYFA2_ÜRÜNLİSTELE


        private void btn2Goster_Click(object sender, EventArgs e)
        {
            byte _UrunTur = 0;

            if (chk2Koltuk2.Checked && chk2Mobilya2.Checked)
                _UrunTur = 0;
            else if (chk2Mobilya2.Checked)
                _UrunTur = 1;
            else if (chk2Koltuk2.Checked)
                _UrunTur = 2;


            dataGridView2.DataSource = UrunIslemler.UrunGetirSec(_UrunTur);
        }

        #endregion

        #region SAYFA3_ÜRÜN

        void DGV3UrunYenile()
        {
            dataGridView3.DataSource = UrunIslemler.UrunGetir();
        }

        private void Btn3UrunOlustur_Click(object sender, EventArgs e)
        {

            try
            {
                bool Tur = false;
                if (rd3Mobilya.Checked)
                    Tur = true;
                if (rd3Koltuk.Checked)
                    Tur = false;

                Urun _Urun = new Urun();
                _Urun.UrunAd = Txt3ürünAd.Text;
                _Urun.UrunAdet = Convert.ToInt32(Txt3ÜrünAdet.Text);
                _Urun.UrunFiyat = Convert.ToInt32(Txt3ÜrünFiyat.Text);
                _Urun.UrunTur = Tur;

                UrunIslemler.UrunOlustur(_Urun);
                DGV3UrunYenile();
                MessageBox.Show("Oluşturuldu");
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem gerçekleştirilemedi.\nLütfen alanları kontrol ediniz", "UYARI !", MessageBoxButtons.OK);
            }
            finally
            {
                if (UrunIslemler.Baglanti.State == ConnectionState.Open)
                {
                    UrunIslemler.Baglanti.Close();
                }
                UrunTemizle();
            }



        }

        private void Btn3UrunSil_Click(object sender, EventArgs e)
        {
            try
            {
                Urun _Urun = new Urun();
                _Urun.UrunID = Convert.ToInt32(txt3ÜrünId.Text);

                UrunIslemler.UrunSil(_Urun);
                DGV3UrunYenile();
                MessageBox.Show("Silindi");
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem gerçekleştirilemedi.\nLütfen alanları kontrol ediniz", "UYARI !", MessageBoxButtons.OK);
            }
            finally
            {
                if (UrunIslemler.Baglanti.State == ConnectionState.Open)
                {
                    UrunIslemler.Baglanti.Close();
                }
                UrunTemizle();
            }



        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            int secilen3 = dataGridView3.SelectedCells[0].RowIndex;
            txt3ÜrünId.Text = dataGridView3.Rows[secilen3].Cells[0].Value.ToString();
            Txt3ürünAd.Text = dataGridView3.Rows[secilen3].Cells[1].Value.ToString();
            Txt3ÜrünAdet.Text = dataGridView3.Rows[secilen3].Cells[2].Value.ToString();
            Txt3ÜrünFiyat.Text = dataGridView3.Rows[secilen3].Cells[3].Value.ToString();
            if (dataGridView3.Rows[secilen3].Cells[4].Value.ToString() == "False")
            {
                rd3Koltuk.Checked = true;
            }
            if (dataGridView3.Rows[secilen3].Cells[4].Value.ToString() == "True")
            {
                rd3Mobilya.Checked = true;

            }

            //dataGridView1.AllowUserToAddRows = false;  son satırı gizleme
            //dataGridView1.ReadOnly = true;    //hücre değişikliğini kapatıyor


        }

        private void btn3UrunGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                bool Tur = false;
                if (rd3Mobilya.Checked)
                    Tur = true;
                if (rd3Koltuk.Checked)
                    Tur = false;

                Urun _Urun = new Urun();
                _Urun.UrunAd = Txt3ürünAd.Text;
                _Urun.UrunAdet = Convert.ToInt32(Txt3ÜrünAdet.Text);
                _Urun.UrunFiyat = Convert.ToInt32(Txt3ÜrünFiyat.Text);
                _Urun.UrunTur = Tur;
                _Urun.UrunID = Convert.ToInt32(txt3ÜrünId.Text);

                UrunIslemler.UrunGuncelle(_Urun);
                DGV3UrunYenile();
                MessageBox.Show("Güncellendi");

            }
            catch (Exception)
            {
                MessageBox.Show("İşlem gerçekleştirilemedi.\nLütfen alanları kontrol ediniz", "UYARI !", MessageBoxButtons.OK);
            }
            finally
            {
                if (UrunIslemler.Baglanti.State == ConnectionState.Open)
                {
                    UrunIslemler.Baglanti.Close();
                    UrunTemizle();
                }
            }




        }

        void UrunTemizle()
        {
            Txt3ürünAd.Text = "";
            Txt3ÜrünAdet.Text = "";
            Txt3ÜrünFiyat.Text = "";
            txt3ÜrünId.Text = "";
            rd3Koltuk.Checked = false;
            rd3Mobilya.Checked = false;
        }








        #endregion

        #region SAYFA4_MÜŞTERİ_ALIM_BİLGİLERİ

        private void cmb4Musteri_Click(object sender, EventArgs e)
        {
            try
            {
                cmb4Musteri.Items.Clear();
                foreach (var item in MusteriIslemler.MusteriGetirSec())
                {
                    cmb4Musteri.Items.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("4. Sayfada Musteri Eklemede Hata");
            }



        }

        private void cmb4Musteri_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView4.DataSource = MusteriIslemler.MusteriGetirIsmeGore(cmb4Musteri.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("4. Sayfada Satış bilgileri eklemede Hata");

            }



        }

        #endregion

        #region SAYFA5_MÜŞTERİ_İSLEMLER


        void DGV5MusteriYenile()
        {
            dataGridView5.DataSource = MusteriIslemler.MusteriGetir();
        }

        void MusteriTemizle()
        {
            Txt5MusteriTC.Text = "";
            cmb5MusteriIl.Text = "";

            txt5MusteriID.Text = "";
            Txt5MusteriAd.Text = "";
        }

        public enum Sehir
        {
            Adana = 1, Adıyaman, Afyonkarahisar, Ağrı, Aksaray, Amasya, Ankara, Antalya, Ardahan, Artvin, Aydın, Balıkesir, Bartın, Batman, Bayburt, Bilecik, Bingöl, Bitlis, Bolu, Burdur, Bursa, Çanakkale, Çankırı, Çorum, Denizli, Diyarbakır, Düzce, Edirne, Elazığ, Erzincan, Erzurum, Eskişehir, Gaziantep, Giresun, Gümüşhane, Hakkâri, Hatay, Iğdır, Isparta, İstanbul, İzmir, Kahramanmaraş, Karabük, Karaman, Kars, Kastamonu, Kayseri, Kilis, Kırıkkale, Kırklareli, Kırşehir, Kocaeli, Konya, Kütahya, Malatya, Manisa, Mardin, Mersin, Muğla, Muş, Nevşehir, Niğde, Ordu, Osmaniye, Rize, Sakarya, Samsun, Şanlıurfa, Siirt, Sinop, Sivas, Şırnak, Tekirdağ, Tokat, Trabzon, Tunceli, Uşak, Van, Yalova, Yozgat, Zonguldak
        }
        void Cmb5MusteriIlDoldur()
        {
            string[] sehirler = Enum.GetNames(typeof(Sehir));
            foreach (string item in sehirler)
            {
                // ComboBox kontrolüne eklenir.
                cmb5MusteriIl.Items.Add(item);

            }

        }

        private void dataGridView5_Click(object sender, EventArgs e)
        {
            int secilen5 = dataGridView5.SelectedCells[0].RowIndex;

            txt5MusteriID.Text = dataGridView5.Rows[secilen5].Cells[0].Value.ToString();
            Txt5MusteriAd.Text = dataGridView5.Rows[secilen5].Cells[1].Value.ToString();
            Txt5MusteriTC.Text = dataGridView5.Rows[secilen5].Cells[2].Value.ToString();
            cmb5MusteriIl.Text = dataGridView5.Rows[secilen5].Cells[3].Value.ToString();

        }

        private void Btn5MüşteriOluştur_Click(object sender, EventArgs e)
        {
            try
            {
                Musteri _Musteri = new Musteri();
                _Musteri.MusteriAd = Txt5MusteriAd.Text;
                _Musteri.MusteriTc = Txt5MusteriTC.Text;
                _Musteri.Musteriİl = cmb5MusteriIl.Text;

                MusteriIslemler.MusteriOlustur(_Musteri);
                DGV5MusteriYenile();
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem gerçekleştirilemedi.\nLütfen alanları kontrol ediniz", "UYARI !", MessageBoxButtons.OK);
            }
            finally
            {
                if (MusteriIslemler.Baglanti.State == ConnectionState.Open)
                {
                    MusteriIslemler.Baglanti.Close();
                }
                MusteriTemizle();
            }

        }

        private void Btn5MüşteriSil_Click(object sender, EventArgs e)
        {

            try
            {
                Musteri _Musteri = new Musteri();
                _Musteri.MusteriID = Convert.ToInt32(txt5MusteriID.Text);

                MusteriIslemler.MusteriSil(_Musteri);
                MessageBox.Show("Müsteri Silme Başarılı");
                DGV5MusteriYenile();
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem gerçekleştirilemedi.\nLütfen alanları kontrol ediniz", "UYARI !", MessageBoxButtons.OK);
            }
            finally
            {
                if (MusteriIslemler.Baglanti.State == ConnectionState.Open)
                {
                    MusteriIslemler.Baglanti.Close();
                }
                MusteriTemizle();
            }




        }

        private void Btn5MüşteriGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Musteri _Musteri = new Musteri();
                _Musteri.MusteriID = Convert.ToInt32(txt5MusteriID.Text);
                _Musteri.MusteriAd = Txt5MusteriAd.Text;
                _Musteri.MusteriTc = (Txt5MusteriTC.Text);
                _Musteri.Musteriİl = cmb5MusteriIl.Text;

                MusteriIslemler.MusteriGuncelle(_Musteri);
                DGV5MusteriYenile();
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem gerçekleştirilemedi.\nLütfen alanları kontrol ediniz", "UYARI !", MessageBoxButtons.OK);
            }
            finally
            {
                if (MusteriIslemler.Baglanti.State == ConnectionState.Open)
                {
                    MusteriIslemler.Baglanti.Close();
                }
                MusteriTemizle();
            }




        }

        #endregion

        #region SAYFA6_SATIŞ_İŞLEMLER

        void DGV6SatisDoldur()
        {
            dataGridView6.DataSource = SatisSonrasıIslemler.SatisGetir();
        }

        string SatisID = "";
        private void dataGridView6_Click(object sender, EventArgs e)
        {
            int secilen = dataGridView6.SelectedCells[0].RowIndex;
            SatisID = dataGridView6.Rows[secilen].Cells[0].Value.ToString();

            lbl6SecilenSatir.Text = "Seçilen Satış: " + SatisID;
        }

        private void btn6SatisSil_Click(object sender, EventArgs e)
        {
            if (SatisID != "")
            {
                SatisSonrasıIslemler.SatisSil(SatisID);
            }
            else
            {
                lbl6SecilenSatir.Text = "Lütfen Silinecek Satırı Seçiniz. !";
            }
            DGV6SatisDoldur();
        }
        private void btn6VTkontrol_Click(object sender, EventArgs e)
        {
            int kont = VeriTabani.VTKontrol();

            if (kont == 1)
                MessageBox.Show("Veritabanı bulunmaktadır.");
            else if(kont==0)
                MessageBox.Show("Veritabanı bulunmamaktadır !");
            else if(kont ==2)
                MessageBox.Show("HATA ! ");

        }

        private void btn6VTOluştur_Click(object sender, EventArgs e)
        {
            MessageBox.Show(VeriTabani.VTOlustur());
        }

        private void btn6DGVyenile_Click(object sender, EventArgs e)
        {
            DGV6SatisDoldur();
        }


        #endregion



    }

}
