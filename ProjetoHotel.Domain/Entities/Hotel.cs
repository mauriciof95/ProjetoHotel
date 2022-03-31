using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("hotel")]
    public class Hotel : BaseEntity
    {
        [Column("nome", Order = 1)]
        [Required(ErrorMessage = "Esse Campo é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo \"{0}\" deve ter no minimo {1} caracteres.")]
        public string Nome { get; set; }

        [Column("cnpj", Order = 2)]
        public string CNPJ { get; set; }

        [Column("endereco", Order = 1)]
        public string Endereco { get; set; }

        [Column("descricao", Order = 1)]
        public string Descricao { get; set; }

        public ICollection<HotelImagem> Imagens { get; set; }
        public ICollection<Quarto> Quartos { get; set; }
    }
}
