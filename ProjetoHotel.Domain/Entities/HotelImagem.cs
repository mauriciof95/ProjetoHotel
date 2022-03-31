using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjetoHotel.Domain.Entities
{
    [Table("hotel_imagem")]
    public class HotelImagem : BaseEntity
    {

        [Column("image_url", Order = 1)]
        public string Image_Url { get; set; }

        [Column("hotel_id", Order = 2)]
        public long Hotel_Id { get; set; }



        [ForeignKey("hotel_id")]
        public Hotel Hotel { get; set; }
    }
}
