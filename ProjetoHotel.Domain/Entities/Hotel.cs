using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("hotel")]
    public class Hotel : BaseModel
    {
        [Column("nome", Order = 1)]
        public string nome { get; set; }

        public ICollection<Quarto> quartos { get; set; }
    }
}
