using ProjetoHotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHotel.Domain.Interfaces
{
    public interface IHotelServices
    {
        Task<Hotel> Cadastrar(Hotel obj, List<string> images = null);
        Task<ICollection<Hotel>> ListarTudo();
        Task<Hotel> BuscarPorId(long id);
        Task<Hotel> Editar(Hotel obj, long id);
        Task Deletar(long id);
    }
}
