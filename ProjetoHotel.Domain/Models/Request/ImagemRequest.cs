using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.ComponentModel.DataAnnotations;

namespace ProjetoHotel.Domain.Models.Request
{
    public class ImagemRequest
    {
        [Required(ErrorMessage = "Selecione uma Imagem."),
            FileExtensions(Extensions = "jpg,png,jpeg", ErrorMessage = "A imagem deve ter os formatos {0}")]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public IFormFile Imagem { get; set; }
    }
}
