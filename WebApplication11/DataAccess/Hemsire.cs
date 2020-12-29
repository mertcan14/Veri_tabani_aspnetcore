using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("hemsire")]
    [Index(nameof(ÇalıstığıBrim), Name = "index_çalıstığı brim")]
    [Index(nameof(AdSoyad), Name = "unique_hemsire_ad soyad", IsUnique = true)]
    public partial class Hemsire
    {
        public Hemsire()
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
        [Column("sifre")]
        public string Sifre { get; set; }
        [Column("cinsiyet")]
        [StringLength(2044)]
        public string Cinsiyet { get; set; }
        [Column("dogum tarihi", TypeName = "date")]
        public DateTime DogumTarihi { get; set; }
        [Required]
        [Column("çalıstığı brim")]
        public string ÇalıstığıBrim { get; set; }

        [ForeignKey(nameof(TCNo))]
        [InverseProperty(nameof(Kisi.Hemsires))]
        public virtual Kisi TCNoNavigation { get; set; }
        [ForeignKey(nameof(ÇalıstığıBrim))]
        [InverseProperty(nameof(Poliklinik.Hemsires))]
        public virtual Poliklinik ÇalıstığıBrimNavigation { get; set; }
        public virtual ICollection<Hastum> Hasta { get; set; }
        public virtual ICollection<Tedavi> Tedavis { get; set; }
    }
}
