using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models.Request;
using System.Collections.Generic;

namespace ProjetoHotel.Domain.Models.ViewModel
{
    public class GalleryViewModel
    {
        //public ImagemRequest Nova_Imagem { get; set; }
        public string Nome_Hotel { get; set; }
        public List<HotelImagem> Imagens{ get; set; }
    }
}
