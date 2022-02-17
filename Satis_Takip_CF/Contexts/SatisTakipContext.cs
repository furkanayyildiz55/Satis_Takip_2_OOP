using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // DbContext Sınıfını kalıtabilmek için
using System.Data.Entity.ModelConfiguration.Conventions;  
using Satis_Takip_CF.Tablolar;

namespace Satis_Takip_CF.Contexts
{
    class SatisTakipContext :DbContext
    {
        public DbSet<Musteri> Musteri { get; set; }

        public DbSet<Urun> Urun { get; set; }

        public DbSet<Satis> Satis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  //pluralizing olayını iptal ediyoruz 
        }

    }
}
