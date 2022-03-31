using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Services
{
    public class HotelServices
    {
        private HotelRepository _repository;
        private HotelImagemRepository _repositoryHotelImagem;

        public HotelServices(SqlDbContext context)
        {
            _repository = new HotelRepository(context);
            _repositoryHotelImagem = new HotelImagemRepository(context);
        }


        public async Task<Hotel> Cadastrar(Hotel obj, List<string> imagem_names = null)
        {
            if (imagem_names != null && imagem_names.Count() > 0)
            {
                obj.Imagens = new List<HotelImagem>();
                foreach (string nome in imagem_names)
                {

                    obj.Imagens.Add(new HotelImagem
                    {
                        Image_Url = nome
                    });
                }
            }
            return await _repository.Cadastrar(obj);
        }

        public async Task<ICollection<Hotel>> ListarTudo()
            => await _repository.ListarTudo();

        public async Task<Hotel> BuscarPorId(long id)
            => await _repository.BuscarPorId(id);

        public async Task<Hotel> Editar(Hotel obj, long id)
            => await _repository.Editar(obj, id);

        public async Task Deletar(long id)
            => await _repository.DeletarTudo(id);
        
    }
}
