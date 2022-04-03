using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("quarto")]
    public class Quarto : BaseEntity
    {
        [Column("nome", Order = 1)]
        [Required(ErrorMessage = "O Campo \"{0}\" é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo \"{0}\" deve ter no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "O campo \"{0}\" deve ter no maximo {1} caracteres.")]
        public string Nome { get; set; }



        [Column("numero_ocupantes", Order = 2)]
        [Range(1, 10, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
        [Required(ErrorMessage = "O campo \"{0}\" é obrigatório")]
        [Display(Name = "Nº de Ocupantes")]
        public int Numero_Ocupantes { get; set; }



        [Column("numero_adultos", Order = 3)]
        [Range(1, 10, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
        [Required(ErrorMessage = "O campo \"{0}\" é obrigatório")]
        [Display(Name = "Nº de Adultos")]
        public int Numero_Adultos { get; set; }



        [Column("numero_criancas", Order = 4)]
        [Range(1, 10, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
        [Required(ErrorMessage = "O campo \"{0}\" é obrigatório")]
        [Display(Name = "Nº de Crianças")]
        public int Numero_Criancas { get; set; }



        [Column("preco", Order = 5)]
        [Required(ErrorMessage = "O campo \"{0}\" é obrigatório")]
        [Range(1, double.MaxValue, ErrorMessage = "O Campo \"{0}\" deve ser maior que 0.")]
        [Display(Name = "Preço R$")]
        public decimal Preco { get; set; }



        [Column("hotel_id", Order = 6)]
        [Required(ErrorMessage = "Selecione um hotel.")]
        public virtual long Hotel_Id { get; set; }



        //nota: aparentemente o nome tem que ser o mesmo da prop respeitando CamelCase, e não o nome da coluna, para o EF nao gerar 2 fk
        [ForeignKey("Hotel_Id")]
        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<QuartoImagem> Imagens { get; set; }

    }
}
