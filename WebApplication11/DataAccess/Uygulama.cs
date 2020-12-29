using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("uygulama")]
    public partial class Uygulama
    {
        public Uygulama()
        {
            Iletisims = new HashSet<Iletisim>();
            Kisis = new HashSet<Kisi>();
        }

        [Key]
        [Column("uygulama ismi")]
        public string UygulamaIsmi { get; set; }
        [Required]
        [Column("hastane ismi")]
        public string HastaneIsmi { get; set; }

        public virtual Hastane HastaneIsmiNavigation { get; set; }
        [InverseProperty(nameof(Iletisim.UygulamaIsmiNavigation))]
        public virtual ICollection<Iletisim> Iletisims { get; set; }
        [InverseProperty(nameof(Kisi.UygulamaIsmiNavigation))]
        public virtual ICollection<Kisi> Kisis { get; set; }
    }
}
