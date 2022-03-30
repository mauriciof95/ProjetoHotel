using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("quarto")]
    public class Quarto : BaseModel
    {
        [Column("nome", Order = 1)]
        public string Nome { get; set; }

        [Column("hotel_id", Order = 4)]
        [Required]
        public long hotel_id { get; set; }



        [ForeignKey("hotel_id")]
        public Hotel hotel { get; set; }
    }
}
