using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjetoHotel.Domain.Entities
{
    [Table("quarto_imagem")]
    public class QuartoImagem : BaseEntity
    {

        [Column("image_url", Order = 1)]
        public string Image_Url { get; set; }

        [Column("quarto_id", Order = 2)]
        public long Quarto_Id { get; set; }



        [ForeignKey("quarto_id")]
        public Quarto Quarto { get; set; }
    }
}
