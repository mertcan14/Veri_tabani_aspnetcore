using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("doktor")]
    [Index(nameof(CalıstığıBrim), Name = "index_calıstığı brim")]
    [Index(nameof(IlgilenenYönetici), Name = "index_ilgilenen yönetici")]
    [Index(nameof(AdSoyad), Name = "unique_doktor_ad soyad", IsUnique = true)]
    public partial class Doktor
    {
        public Doktor()
        {
            Hasta = new HashSet<Hastum>();
            Tedavis = new HashSet<Tedavi>();
        }

        [Key]
        [Column("T.C no")]
        public long TCNo { get; set; }
        [Key]
        [Column("ad soyad")]
        public string AdSoyad { get; set; }
        [Required]
        [Column("cinsiyet")]
        [StringLength(2044)]
        public string Cinsiyet { get; set; }
        [Column("dogum tarihi", TypeName = "date")]
        public DateTime DogumTarihi { get; set; }
        [Required]
        [Column("calıstığı brim")]
        public string CalıstığıBrim { get; set; }
        [Required]
        [Column("sifre")]
        public string Sifre { get; set; }
        [Required]
        [Column("ilgilenen yönetici")]
        public string IlgilenenYönetici { get; set; }

        [ForeignKey(nameof(CalıstığıBrim))]
        [InverseProperty(nameof(Poliklinik.Doktors))]
        public virtual Poliklinik CalıstığıBrimNavigation { get; set; }
        public virtual Yönetici IlgilenenYöneticiNavigation { get; set; }
        [ForeignKey(nameof(TCNo))]
        [InverseProperty(nameof(Kisi.Doktors))]
        public virtual Kisi TCNoNavigation { get; set; }
        public virtual ICollection<Hastum> Hasta { get; set; }
        public virtual ICollection<Tedavi> Tedavis { get; set; }
    }
}
