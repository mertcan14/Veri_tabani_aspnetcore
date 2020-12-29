using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication11.DataAccess
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doktor> Doktors { get; set; }
        public virtual DbSet<HastaYakını> HastaYakınıs { get; set; }
        public virtual DbSet<Hastane> Hastanes { get; set; }
        public virtual DbSet<Hastum> Hasta { get; set; }
        public virtual DbSet<Hemsire> Hemsires { get; set; }
        public virtual DbSet<Ilac> Ilacs { get; set; }
        public virtual DbSet<Iletisim> Iletisims { get; set; }
        public virtual DbSet<Kisi> Kisis { get; set; }
        public virtual DbSet<Poliklinik> Polikliniks { get; set; }
        public virtual DbSet<Sehir> Sehirs { get; set; }
        public virtual DbSet<Tedarikci> Tedarikcis { get; set; }
        public virtual DbSet<Tedavi> Tedavis { get; set; }
        public virtual DbSet<Uygulama> Uygulamas { get; set; }
        public virtual DbSet<Yönetici> Yöneticis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=Hastane;Username=postgres;Password=mert.1414");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_Turkey.1254");

            modelBuilder.Entity<Doktor>(entity =>
            {
                entity.HasKey(e => new { e.TCNo, e.AdSoyad })
                    .HasName("unique_doktor_T.C no");

                entity.Property(e => e.Cinsiyet).IsFixedLength(true);

                entity.HasOne(d => d.CalıstığıBrimNavigation)
                    .WithMany(p => p.Doktors)
                    .HasForeignKey(d => d.CalıstığıBrim)
                    .HasConstraintName("lnk_poliklinik_doktor");

                entity.HasOne(d => d.IlgilenenYöneticiNavigation)
                    .WithMany(p => p.Doktors)
                    .HasPrincipalKey(p => p.AdSoyad)
                    .HasForeignKey(d => d.IlgilenenYönetici)
                    .HasConstraintName("lnk_yönetici_doktor");

                entity.HasOne(d => d.TCNoNavigation)
                    .WithMany(p => p.Doktors)
                    .HasForeignKey(d => d.TCNo)
                    .HasConstraintName("lnk_kisi_doktor");
            });

            modelBuilder.Entity<HastaYakını>(entity =>
            {
                entity.HasKey(e => e.TCNo)
                    .HasName("unique_hasta yakını _T.C no");

                entity.Property(e => e.TCNo).ValueGeneratedNever();

                entity.HasOne(d => d.HastaTCNavigation)
                    .WithOne(p => p.HastaYakını)
                    .HasForeignKey<HastaYakını>(d => d.HastaTC)
                    .HasConstraintName("lnk_hasta_hasta yakını ");

                entity.HasOne(d => d.TCNoNavigation)
                    .WithOne(p => p.HastaYakını)
                    .HasForeignKey<HastaYakını>(d => d.TCNo)
                    .HasConstraintName("lnk_kisi_hasta yakını ");
            });

            modelBuilder.Entity<Hastane>(entity =>
            {
                entity.HasKey(e => new { e.VergiNumarası, e.Isim })
                    .HasName("hastane_pkey");

                entity.HasOne(d => d.SehirIsmiNavigation)
                    .WithOne(p => p.Hastane)
                    .HasForeignKey<Hastane>(d => d.SehirIsmi)
                    .HasConstraintName("lnk_sehir_hastane");

                entity.HasOne(d => d.YöneticileriNavigation)
                    .WithOne(p => p.HastaneNavigation)
                    .HasPrincipalKey<Yönetici>(p => p.AdSoyad)
                    .HasForeignKey<Hastane>(d => d.Yöneticileri)
                    .HasConstraintName("lnk_yönetici_hastane");
            });

            modelBuilder.Entity<Hastum>(entity =>
            {
                entity.HasKey(e => e.Tcno)
                    .HasName("unique_hasta_T.C no");

                entity.Property(e => e.Tcno).ValueGeneratedNever();

                entity.Property(e => e.Cinsiyet).IsFixedLength(true);

                entity.HasOne(d => d.IlgilenendoktorNavigation)
                    .WithMany(p => p.Hasta)
                    .HasPrincipalKey(p => p.AdSoyad)
                    .HasForeignKey(d => d.Ilgilenendoktor)
                    .HasConstraintName("lnk_doktor_hasta");

                entity.HasOne(d => d.IlgilenenhemsireNavigation)
                    .WithMany(p => p.Hasta)
                    .HasPrincipalKey(p => p.AdSoyad)
                    .HasForeignKey(d => d.Ilgilenenhemsire)
                    .HasConstraintName("lnk_hemsire_hasta");

                entity.HasOne(d => d.TcnoNavigation)
                    .WithOne(p => p.Hastum)
                    .HasForeignKey<Hastum>(d => d.Tcno)
                    .HasConstraintName("lnk_kisi_hasta");
            });

            modelBuilder.Entity<Hemsire>(entity =>
            {
                entity.HasKey(e => new { e.TCNo, e.AdSoyad })
                    .HasName("unique_hemsire_T.C no");

                entity.Property(e => e.Cinsiyet).IsFixedLength(true);

                entity.HasOne(d => d.TCNoNavigation)
                    .WithMany(p => p.Hemsires)
                    .HasForeignKey(d => d.TCNo)
                    .HasConstraintName("lnk_kisi_hemsire");

                entity.HasOne(d => d.ÇalıstığıBrimNavigation)
                    .WithMany(p => p.Hemsires)
                    .HasForeignKey(d => d.ÇalıstığıBrim)
                    .HasConstraintName("lnk_poliklinik_hemsire");
            });

            modelBuilder.Entity<Ilac>(entity =>
            {
                entity.HasKey(e => e.IlacIsmi)
                    .HasName("unique_ilac_ilac ismi");

                entity.HasOne(d => d.FirmaIsmiNavigation)
                    .WithMany(p => p.Ilacs)
                    .HasForeignKey(d => d.FirmaIsmi)
                    .HasConstraintName("lnk_tedarikci_ilac");

                entity.HasOne(d => d.HastaTCNoNavigation)
                    .WithOne(p => p.Ilac)
                    .HasForeignKey<Ilac>(d => d.HastaTCNo)
                    .HasConstraintName("lnk_Tedavi_ilac");
            });

            modelBuilder.Entity<Iletisim>(entity =>
            {
                entity.HasKey(e => e.TCNo)
                    .HasName("unique_iletisim_T.C no");

                entity.Property(e => e.TCNo).ValueGeneratedNever();

                entity.HasOne(d => d.TCNoNavigation)
                    .WithOne(p => p.Iletisim)
                    .HasForeignKey<Iletisim>(d => d.TCNo)
                    .HasConstraintName("lnk_kisi_iletisim");

                entity.HasOne(d => d.UygulamaIsmiNavigation)
                    .WithMany(p => p.Iletisims)
                    .HasForeignKey(d => d.UygulamaIsmi)
                    .HasConstraintName("lnk_uygulama_iletisim");
            });

            modelBuilder.Entity<Kisi>(entity =>
            {
                entity.HasKey(e => e.TCNo)
                    .HasName("unique_kisi_T.C no");

                entity.Property(e => e.TCNo).ValueGeneratedNever();

                entity.Property(e => e.Cinsiyet).IsFixedLength(true);

                entity.HasOne(d => d.UygulamaIsmiNavigation)
                    .WithMany(p => p.Kisis)
                    .HasForeignKey(d => d.UygulamaIsmi)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("lnk_uygulama_kisi");
            });

            modelBuilder.Entity<Poliklinik>(entity =>
            {
                entity.HasKey(e => e.BölümIsmi)
                    .HasName("unique_poliklinik_bölüm ismi");

                entity.HasOne(d => d.HastaneIsmiNavigation)
                    .WithMany(p => p.Polikliniks)
                    .HasPrincipalKey(p => p.Isim)
                    .HasForeignKey(d => d.HastaneIsmi)
                    .HasConstraintName("lnk_hastane_poliklinik");
            });

            modelBuilder.Entity<Sehir>(entity =>
            {
                entity.HasKey(e => e.Isim)
                    .HasName("unique_sehir_isim");
            });

            modelBuilder.Entity<Tedarikci>(entity =>
            {
                entity.HasKey(e => e.FirmaIsmi)
                    .HasName("unique_tedarikci_firma ismi");
            });

            modelBuilder.Entity<Tedavi>(entity =>
            {
                entity.HasKey(e => e.HastaTCNo)
                    .HasName("Tedavi_pkey");

                entity.Property(e => e.HastaTCNo).ValueGeneratedNever();

                entity.HasOne(d => d.HastaTCNoNavigation)
                    .WithOne(p => p.Tedavi)
                    .HasForeignKey<Tedavi>(d => d.HastaTCNo)
                    .HasConstraintName("lnk_hasta_Tedavi");

                entity.HasOne(d => d.IlgilenenDoktorNavigation)
                    .WithMany(p => p.Tedavis)
                    .HasPrincipalKey(p => p.AdSoyad)
                    .HasForeignKey(d => d.IlgilenenDoktor)
                    .HasConstraintName("lnk_doktor_Tedavi");

                entity.HasOne(d => d.IlgilenenHemsireNavigation)
                    .WithMany(p => p.Tedavis)
                    .HasPrincipalKey(p => p.AdSoyad)
                    .HasForeignKey(d => d.IlgilenenHemsire)
                    .HasConstraintName("lnk_hemsire_Tedavi");
            });

            modelBuilder.Entity<Uygulama>(entity =>
            {
                entity.HasKey(e => e.UygulamaIsmi)
                    .HasName("unique_uygulama_uygulama ismi");

                entity.HasOne(d => d.HastaneIsmiNavigation)
                    .WithMany(p => p.Uygulamas)
                    .HasPrincipalKey(p => p.Isim)
                    .HasForeignKey(d => d.HastaneIsmi)
                    .HasConstraintName("lnk_hastane_uygulama");
            });

            modelBuilder.Entity<Yönetici>(entity =>
            {
                entity.HasKey(e => new { e.TCNo, e.AdSoyad })
                    .HasName("yönetici_pkey");

                entity.Property(e => e.Cinsiyet).IsFixedLength(true);

                entity.HasOne(d => d.TCNoNavigation)
                    .WithOne(p => p.Yönetici)
                    .HasForeignKey<Yönetici>(d => d.TCNo)
                    .HasConstraintName("lnk_kisi_yönetici");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
