using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("quarto_imagem")]
    public class QuartoImagem : BaseEntity
    {

        [Column("image_url", Order = 1)]
        [Required]
        public string Image_Url { get; set; }

        [Column("quarto_id", Order = 2)]
        public long Quarto_Id { get; set; }



        [ForeignKey("Quarto_Id")]
        public Quarto Quarto { get; set; }
    }
}
