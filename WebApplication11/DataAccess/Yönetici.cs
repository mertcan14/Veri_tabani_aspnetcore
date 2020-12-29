using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("yönetici")]
    [Index(nameof(TCNo), Name = "index_T.C no")]
    [Index(nameof(TCNo), Name = "index_T.C no1")]
    [Index(nameof(TCNo), Name = "unique_yönetici_T.C no", IsUnique = true)]
    [Index(nameof(AdSoyad), Name = "unique_yönetici_ad soyad", IsUnique = true)]
    public partial class Yönetici
    {
        public Yönetici()
        {
            Doktors = new HashSet<Doktor>();
        }

        [Key]
        [Column("T.C no")]
        public long TCNo { get; set; }
        [Key]
        [Column("ad soyad")]
        public string AdSoyad { get; set; }
        [Column("cinsiyet")]
        [StringLength(2044)]
        public string Cinsiyet { get; set; }
        [Column("dogum tarihi", TypeName = "date")]
        public DateTime DogumTarihi { get; set; }
        [Required]
        [Column("sifre")]
        public string Sifre { get; set; }
        [Required]
        [Column("hastane")]
        public string Hastane { get; set; }

        [ForeignKey(nameof(TCNo))]
        [InverseProperty(nameof(Kisi.Yönetici))]
        public virtual Kisi TCNoNavigation { get; set; }
        public virtual Hastane HastaneNavigation { get; set; }
        public virtual ICollection<Doktor> Doktors { get; set; }
    }
}
