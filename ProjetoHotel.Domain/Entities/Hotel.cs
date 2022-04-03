using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("hotel")]
    public class Hotel : BaseEntity
    {
        [Column("nome", Order = 1)]
        [Required(ErrorMessage = "\"{0}\" é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo \"{0}\" deve ter no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "O campo \"{0}\" deve ter no maximo {1} caracteres.")]
        public string Nome { get; set; }

        [Column("cnpj", Order = 2)]
        [Required(ErrorMessage = "\"{0}\" é obrigatório")]
        [MinLength(14, ErrorMessage = "O campo \"{0}\" deve ter {1} digitos.")]
        [MaxLength(14, ErrorMessage = "O campo \"{0}\" deve ter {1} digitos.")]
        [Range(0, long.MaxValue, ErrorMessage = "Insira apenas numeros")]
        public string CNPJ { get; set; }

        [Column("endereco", Order = 3)]
        [Required(ErrorMessage = "\"{0}\" é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo \"{0}\" deve ter no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"{0}\" deve ter no maximo {1} caracteres.")]
        public string Endereco { get; set; }

        [Column("descricao", Order = 4)]
        public string Descricao { get; set; }

        
        public ICollection<HotelImagem> Imagens { get; set; }

        public ICollection<Quarto> Quartos { get; set; }
    }
}
