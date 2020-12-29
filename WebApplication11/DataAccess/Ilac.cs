using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("ilac")]
    [Index(nameof(HastaTCNo), Name = "index_hasta T.C no")]
    [Index(nameof(HastaTCNo), Name = "index_hasta T.C no1")]
    [Index(nameof(HastaTCNo), Name = "unique_ilac_hasta T.C no", IsUnique = true)]
    [Index(nameof(HastaTCNo), Name = "unique_ilac_hasta T.C no1", IsUnique = true)]
    public partial class Ilac
    {
        [Key]
        [Column("ilac ismi")]
        public string IlacIsmi { get; set; }
        [Column("etken ismi")]
        public string EtkenIsmi { get; set; }
        [Column("kullanım amacı")]
        public string KullanımAmacı { get; set; }
        [Required]
        [Column("firma ismi")]
        public string FirmaIsmi { get; set; }
        [Required]
        [Column("tedavi ismi")]
        public string TedaviIsmi { get; set; }
        [Column("hasta T.C no")]
        public long HastaTCNo { get; set; }

        [ForeignKey(nameof(FirmaIsmi))]
        [InverseProperty(nameof(Tedarikci.Ilacs))]
        public virtual Tedarikci FirmaIsmiNavigation { get; set; }
        [ForeignKey(nameof(HastaTCNo))]
        [InverseProperty(nameof(Tedavi.Ilac))]
        public virtual Tedavi HastaTCNoNavigation { get; set; }
    }
}
