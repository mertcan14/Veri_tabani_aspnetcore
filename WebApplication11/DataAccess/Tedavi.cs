using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("Tedavi")]
    [Index(nameof(IlgilenenDoktor), Name = "index_ilgilenen doktor ")]
    [Index(nameof(IlgilenenHemsire), Name = "index_ilgilenen hemsire ")]
    public partial class Tedavi
    {
        [Required]
        [Column("tedavi ismi")]
        public string TedaviIsmi { get; set; }
        [Column("kullanılan ilaç ")]
        public string KullanılanIlaç { get; set; }
        [Column("tedavi süresi")]
        public TimeSpan TedaviSüresi { get; set; }
        [Key]
        [Column("hasta T.C no")]
        public long HastaTCNo { get; set; }
        [Required]
        [Column("ilgilenen doktor ")]
        public string IlgilenenDoktor { get; set; }
        [Required]
        [Column("ilgilenen hemsire ")]
        public string IlgilenenHemsire { get; set; }

        [ForeignKey(nameof(HastaTCNo))]
        [InverseProperty(nameof(Hastum.Tedavi))]
        public virtual Hastum HastaTCNoNavigation { get; set; }
        public virtual Doktor IlgilenenDoktorNavigation { get; set; }
        public virtual Hemsire IlgilenenHemsireNavigation { get; set; }
        [InverseProperty("HastaTCNoNavigation")]
        public virtual Ilac Ilac { get; set; }
    }
}
