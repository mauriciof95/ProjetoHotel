using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHotel.Services
{
    public class HotelServices
    {
        private HotelRepository _repository;
        public HotelServices(SqlDbContext context)
            => _repository = new HotelRepository(context);


        public async Task<Hotel> Cadastrar(Hotel obj)
            => await _repository.Cadastrar(obj);

        public async Task<ICollection<Hotel>> ListarTudo()
            => await _repository.ListarTudo();

        public async Task<Hotel> BuscarPorId(long id)
            => await _repository.BuscarPorId(id);

        public async Task<Hotel> Editar(Hotel obj, long id)
            => await _repository.Editar(obj, id);

        public async Task Deletar(long id)
        {
            await _repository.Deletar(id);
        } 
    }
}
