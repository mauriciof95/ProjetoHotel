using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("hotel_imagem")]
    public class HotelImagem : BaseEntity
    {

        [Column("image_url", Order = 1)]
        [Required]
        public string Image_Url { get; set; }

        [Column("hotel_id", Order = 2)]
        public long Hotel_Id { get; set; }


        //nota: aparentemente o nome tem que ser o mesmo da prop respeitando CamelCase, e não o nome da coluna, para o EF nao gerar 2 fk
        [ForeignKey("Hotel_Id")]
        public Hotel Hotel { get; set; }
    }
}
