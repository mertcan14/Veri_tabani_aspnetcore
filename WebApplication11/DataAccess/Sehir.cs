using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("sehir")]
    public partial class Sehir
    {
        [Key]
        [Column("isim")]
        public string Isim { get; set; }
        [Required]
        [Column("ülke")]
        public string Ülke { get; set; }

        [InverseProperty("SehirIsmiNavigation")]
        public virtual Hastane Hastane { get; set; }
    }
}
