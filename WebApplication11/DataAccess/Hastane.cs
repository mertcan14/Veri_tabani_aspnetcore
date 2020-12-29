using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("hastane")]
    [Index(nameof(VergiNumarası), Name = "index_vergi numarası")]
    [Index(nameof(Isim), Name = "unique_hastane_isim", IsUnique = true)]
    [Index(nameof(SehirIsmi), Name = "unique_hastane_sehir ismi", IsUnique = true)]
    [Index(nameof(VergiNumarası), Name = "unique_hastane_vergi numarası", IsUnique = true)]
    [Index(nameof(Yöneticileri), Name = "unique_hastane_yöneticileri", IsUnique = true)]
    public partial class Hastane
    {
        public Hastane()
        {
            Polikliniks = new HashSet<Poliklinik>();
            Uygulamas = new HashSet<Uygulama>();
        }

        [Key]
        [Column("vergi numarası")]
        public int VergiNumarası { get; set; }
        [Key]
        [Column("isim")]
        public string Isim { get; set; }
        [Column("kurulma tarihi", TypeName = "date")]
        public DateTime KurulmaTarihi { get; set; }
        [Required]
        [Column("sehir ismi")]
        public string SehirIsmi { get; set; }
        [Required]
        [Column("yöneticileri")]
        public string Yöneticileri { get; set; }

        [ForeignKey(nameof(SehirIsmi))]
        [InverseProperty(nameof(Sehir.Hastane))]
        public virtual Sehir SehirIsmiNavigation { get; set; }
        public virtual Yönetici YöneticileriNavigation { get; set; }
        public virtual ICollection<Poliklinik> Polikliniks { get; set; }
        public virtual ICollection<Uygulama> Uygulamas { get; set; }
    }
}
