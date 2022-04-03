using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models.Request;
using System.Collections.Generic;

namespace ProjetoHotel.Domain.Models.ViewModel
{
    public class GalleryQuartoViewModel
    {
        public string Nome_Quarto { get; set; }
        public List<QuartoImagem> Imagens{ get; set; }
    }
}
