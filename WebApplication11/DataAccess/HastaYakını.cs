using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("hasta yakını ")]
    [Index(nameof(HastaTC), Name = "index_hasta T.C")]
    [Index(nameof(HastaTC), Name = "unique_hasta yakını _hasta T.C", IsUnique = true)]
    public partial class HastaYakını
    {
        [Key]
        [Column("T.C no")]
        public long TCNo { get; set; }
        [Required]
        [Column("ad  soyad")]
        public string AdSoyad { get; set; }
        [Column("dogum tarihi", TypeName = "date")]
        public DateTime DogumTarihi { get; set; }
        [Column("hasta T.C")]
        public long HastaTC { get; set; }

        [ForeignKey(nameof(HastaTC))]
        [InverseProperty(nameof(Hastum.HastaYakını))]
        public virtual Hastum HastaTCNavigation { get; set; }
        [ForeignKey(nameof(TCNo))]
        [InverseProperty(nameof(Kisi.HastaYakını))]
        public virtual Kisi TCNoNavigation { get; set; }
    }
}
