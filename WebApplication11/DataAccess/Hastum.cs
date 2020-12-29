using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("hasta")]
    public partial class Hastum
    {
        [Key]
        [Column("TCno")]
        public long Tcno { get; set; }
        [Required]
        [Column("adsoyad")]
        public string Adsoyad { get; set; }
        [Column("cinsiyet")]
        [StringLength(2044)]
        public string Cinsiyet { get; set; }
        [Column("dogumtarihi", TypeName = "date")]
        public DateTime Dogumtarihi { get; set; }
        [Column("meslek")]
        public string Meslek { get; set; }
        [Column("öncedengeçirdiğihastalıklar")]
        public string Öncedengeçirdiğihastalıklar { get; set; }
        [Required]
        [Column("ilgilenendoktor")]
        public string Ilgilenendoktor { get; set; }
        [Required]
        [Column("ilgilenenhemsire")]
        public string Ilgilenenhemsire { get; set; }
        [Column("adres")]
        public string Adres { get; set; }

        public virtual Doktor IlgilenendoktorNavigation { get; set; }
        public virtual Hemsire IlgilenenhemsireNavigation { get; set; }
        [ForeignKey(nameof(Tcno))]
        [InverseProperty(nameof(Kisi.Hastum))]
        public virtual Kisi TcnoNavigation { get; set; }
        [InverseProperty("HastaTCNavigation")]
        public virtual HastaYakını HastaYakını { get; set; }
        [InverseProperty("HastaTCNoNavigation")]
        public virtual Tedavi Tedavi { get; set; }
    }
}
