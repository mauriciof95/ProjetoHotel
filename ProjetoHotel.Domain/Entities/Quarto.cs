using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("quarto")]
    public class Quarto : BaseEntity
    {
        [Column("nome", Order = 1)]
        public string Nome { get; set; }

        [Column("numero_ocupantes", Order = 2)]
        public int Numero_Ocupantes { get; set; }

        [Column("numero_adultos", Order = 3)]
        public int Numero_Adultos { get; set; }

        [Column("numero_criancas", Order = 4)]
        public string Numero_Criancas { get; set; }

        [Column("preco", Order = 5)]
        public decimal Preco { get; set; }

        [Column("hotel_id", Order = 4)]
        [Required]
        public long Hotel_Id { get; set; }


        [ForeignKey("hotel_id")]
        public Hotel Hotel { get; set; }

        public ICollection<QuartoImagem> Imagens { get; set; }

    }
}
