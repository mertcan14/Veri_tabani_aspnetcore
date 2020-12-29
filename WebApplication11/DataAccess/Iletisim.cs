using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("iletisim")]
    public partial class Iletisim
    {
        [Key]
        [Column("T.C no")]
        public long TCNo { get; set; }
        [Column("konu")]
        public string Konu { get; set; }
        [Column("tarih", TypeName = "date")]
        public DateTime Tarih { get; set; }
        [Required]
        [Column("uygulama ismi")]
        public string UygulamaIsmi { get; set; }

        [ForeignKey(nameof(TCNo))]
        [InverseProperty(nameof(Kisi.Iletisim))]
        public virtual Kisi TCNoNavigation { get; set; }
        [ForeignKey(nameof(UygulamaIsmi))]
        [InverseProperty(nameof(Uygulama.Iletisims))]
        public virtual Uygulama UygulamaIsmiNavigation { get; set; }
    }
}
