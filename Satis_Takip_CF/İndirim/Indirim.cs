using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satis_Takip_CF.İndirim
{
    public class Indirim
    {
        private int _Deger;

        public readonly int OgrenciIndirimOrani = 10;
        private int _YuzdeOdakliIndirimOrani;
        private int _FiyatOdakliIndirimDegeri;



        public int Deger { get { return _Deger; } set { _Deger = value; } }

        public int YuzdeOdakliIndirimOrani
        {
            get
            {
                return _YuzdeOdakliIndirimOrani;
            }
            set
            {
                if (value > 100)
                    throw new OranAsımHatası("Yüzde Odaklı indirim oranı 100 değerini geçmemeli !");
                else
                    _YuzdeOdakliIndirimOrani = value;
            }
        }

        public int FiyatOdakliIndirimDegeri
        {
            get
            {
                return _FiyatOdakliIndirimDegeri;
            }
            set
            {
                if (value > 500)
                    throw new OranAsımHatası("Fiyat odaklı indirim değeri, 500 değerini geçmemeli !");
                else
                    _FiyatOdakliIndirimDegeri = value;
            }
        }


        public Indirim()
        {

        }
        public Indirim(int _DEGER)
        {
            _Deger = _DEGER;
        }
        public Indirim(int _DEGER, int _YUZDE)
        {
            Deger = _DEGER;
            YuzdeOdakliIndirimOrani = _YUZDE;
        }
        public Indirim(int _DEGER, int _INDIRIM_DEGER, bool _C)
        {
            Deger = _DEGER;
            FiyatOdakliIndirimDegeri = _INDIRIM_DEGER;
        }


        private int YuzdeHesapla(int _deger, int _yuzde)
        {
            return (_deger / 100) * _yuzde;
        }


        [Obsolete("Kullanmayınız",true )]
        public int OgrenciIndirimHesapla( int _DEGER )
        {
            return _DEGER - YuzdeHesapla(_DEGER,OgrenciIndirimOrani ); 
        } 

        public int OgrenciIndirimHesapla ()
        {
            return Deger - YuzdeHesapla(Deger, OgrenciIndirimOrani);

        }

        public int YuzdeOdakliİndirim()
        {
            return Deger - YuzdeHesapla(Deger, YuzdeOdakliIndirimOrani);
        }

        public int FiyatOdakliIndirim()
        {
            if (Deger>=FiyatOdakliIndirimDegeri)
            {
                return Deger - FiyatOdakliIndirimDegeri;
            }
            else
            {
                throw new OranAsımHatası("İndirim yapılacak değer indirim değerinden büyük olamaz");
            }

            
        }

        




    }
}
