using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("poliklinik")]
    public partial class Poliklinik
    {
        public Poliklinik()
        {
            Doktors = new HashSet<Doktor>();
            Hemsires = new HashSet<Hemsire>();
        }

        [Key]
        [Column("bölüm ismi")]
        public string BölümIsmi { get; set; }
        [Required]
        [Column("hastane ismi")]
        public string HastaneIsmi { get; set; }

        public virtual Hastane HastaneIsmiNavigation { get; set; }
        [InverseProperty(nameof(Doktor.CalıstığıBrimNavigation))]
        public virtual ICollection<Doktor> Doktors { get; set; }
        [InverseProperty(nameof(Hemsire.ÇalıstığıBrimNavigation))]
        public virtual ICollection<Hemsire> Hemsires { get; set; }
    }
}
