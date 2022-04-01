using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Services
{
    public class QuartoServices
    {
        private QuartoRepository _repository;
        private QuartoImagemRepository _repositoryQuartoImagem;

        private HotelServices _servicesHotel;

        public QuartoServices(SqlDbContext context)
        {
            _repository = new QuartoRepository(context);
            _repositoryQuartoImagem = new QuartoImagemRepository(context);
            _servicesHotel = new HotelServices(context);
        }

        public async Task<Quarto> Cadastrar(Quarto obj, List<string> imagem_names = null)
        {
            if (imagem_names != null && imagem_names.Count() > 0)
            {
                obj.Imagens = new List<QuartoImagem>();
                foreach (string nome in imagem_names)
                {

                    obj.Imagens.Add(new QuartoImagem
                    {
                        Image_Url = nome
                    });
                }
            }
            return await _repository.Cadastrar(obj);
        }

        public async Task<List<SelectModel>> RetornarHoteisSelect()
            => await _servicesHotel.RetornarHoteisSelect();
        

        public async Task<ICollection<Quarto>> ListarTudo()
            => await _repository.ListarTudo();

        public async Task<Quarto> BuscarPorId(long id)
            => await _repository.BuscarPorId(id);

        public async Task<Quarto> Editar(Quarto obj, long id)
            => await _repository.Editar(obj, id);

        public async Task Deletar(long id)
            => await _repository.DeletarTudo(id);

    }
}
