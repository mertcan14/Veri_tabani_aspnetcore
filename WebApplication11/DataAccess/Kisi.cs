using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("kisi")]
    [Index(nameof(UygulamaIsmi), Name = "index_uygulama ismi")]
    public partial class Kisi
    {
        public Kisi()
        {
            Doktors = new HashSet<Doktor>();
            Hemsires = new HashSet<Hemsire>();
        }

        [Key]
        [Column("T.C no")]
        public long TCNo { get; set; }
        [Required]
        [Column("ad soyad")]
        public string AdSoyad { get; set; }
        [Required]
        [Column("cinsiyet")]
        [StringLength(2044)]
        public string Cinsiyet { get; set; }
        [Column("dogum tarihi", TypeName = "date")]
        public DateTime DogumTarihi { get; set; }
        [Column("uygulama ismi")]
        public string UygulamaIsmi { get; set; }

        [ForeignKey(nameof(UygulamaIsmi))]
        [InverseProperty(nameof(Uygulama.Kisis))]
        public virtual Uygulama UygulamaIsmiNavigation { get; set; }
        [InverseProperty("TCNoNavigation")]
        public virtual HastaYakını HastaYakını { get; set; }
        [InverseProperty("TcnoNavigation")]
        public virtual Hastum Hastum { get; set; }
        [InverseProperty("TCNoNavigation")]
        public virtual Iletisim Iletisim { get; set; }
        [InverseProperty("TCNoNavigation")]
        public virtual Yönetici Yönetici { get; set; }
        [InverseProperty(nameof(Doktor.TCNoNavigation))]
        public virtual ICollection<Doktor> Doktors { get; set; }
        [InverseProperty(nameof(Hemsire.TCNoNavigation))]
        public virtual ICollection<Hemsire> Hemsires { get; set; }
    }
}
