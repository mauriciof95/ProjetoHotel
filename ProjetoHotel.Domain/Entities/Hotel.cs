using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoHotel.Domain.Entities
{
    [Table("hotel")]
    public class Hotel : BaseEntity
    {
        [Column("nome", Order = 1)]
        public string Nome { get; set; }

        [Column("cnpj", Order = 2)]
        public string CNPJ { get; set; }

        [Column("endereco", Order = 1)]
        public string Endereco { get; set; }

        [Column("descricao", Order = 1)]
        public string Descricao { get; set; }

        [Column("image_url", Order = 1)]
        public string Image_Url { get; set; }

        public ICollection<HotelImagem> Imagens { get; set; }
        public ICollection<Quarto> quartos { get; set; }
    }
}
