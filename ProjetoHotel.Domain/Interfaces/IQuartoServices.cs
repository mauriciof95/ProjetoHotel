using ProjetoHotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHotel.Domain.Interfaces
{
    public interface IQuartoServices
    {
        Task<Quarto> Cadastrar(Quarto obj, List<string> images = null);
        Task<ICollection<Quarto>> ListarTudo();
        Task<Quarto> BuscarPorId(long id);
        Task<Quarto> Editar(Quarto obj, long id);
        Task Deletar(long id);
    }
}
