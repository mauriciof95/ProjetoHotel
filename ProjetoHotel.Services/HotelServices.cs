using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models;
using ProjetoHotel.Domain.Models.Exceptions;
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

        private QuartoServices _quartoServices;

        public HotelServices(SqlDbContext context)
        {
            _repository = new HotelRepository(context);
            _repositoryHotelImagem = new HotelImagemRepository(context);
            _quartoServices = new QuartoServices(context);
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

        public async Task<List<SelectModel>> RetornarHoteisSelect()
            => await _repository.RetornarHoteisSelect();
        

        public async Task<ICollection<Hotel>> ListarTudo()
            => await _repository.ListarTudo();

        public async Task<Hotel> BuscarPorId(long id)
            => await _repository.BuscarPorId(id);

        public async Task<Hotel> Editar(Hotel obj, long id)
            => await _repository.Editar(obj, id);

        public async Task Deletar(long id)
        {
            bool temQuartos = await _quartoServices.TemQuartosPorHotelId(id);

            if (temQuartos) throw new ValidationException("Não é possivel deletar esse registro, pois está associado a outro registros.");

            var Imagens = await _repositoryHotelImagem.RetornarPorHotelId(id);

            if (Imagens.Count() > 0)
            {
                foreach (var img in Imagens)
                {
                    await _repositoryHotelImagem.Deletar(img);
                }
            }
            await _repository.Deletar(id);
        }
    }
}
