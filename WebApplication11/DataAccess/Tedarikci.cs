using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication11.DataAccess
{
    [Table("tedarikci")]
    public partial class Tedarikci
    {
        public Tedarikci()
        {
            Ilacs = new HashSet<Ilac>();
        }

        [Key]
        [Column("firma ismi")]
        public string FirmaIsmi { get; set; }

        [InverseProperty(nameof(Ilac.FirmaIsmiNavigation))]
        public virtual ICollection<Ilac> Ilacs { get; set; }
    }
}
