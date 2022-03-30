using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Domain.Entities
{
    public class BaseModel
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Column("data_criacao", Order = 100)]
        public DateTime Data_criacao { get; set; }

        [Column("data_edicao", Order = 101)]
        public DateTime? Data_edicao { get; set; }
    }
}
