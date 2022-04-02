using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Business
{
    public class QuartoBusiness
    {
        private QuartoRepository _repository;
        private QuartoImagemRepository _repositoryQuartoImagem;

        public QuartoBusiness(SqlDbContext context)
        {
            _repository = new QuartoRepository(context);
            _repositoryQuartoImagem = new QuartoImagemRepository(context);
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

        


        public async Task<ICollection<Quarto>> ListarTudo()
            => await _repository.ListarTudo();

        public async Task<Quarto> BuscarPorId(long id)
            => await _repository.BuscarPorId(id);

        public async Task<Quarto> Editar(Quarto obj, long id)
            => await _repository.Editar(obj, id);

        public async Task Deletar(long id)
        { 

            await _repository.DeletarTudo(id);
        }

        public async Task<bool> TemQuartosPorHotelId(long id)
             => await _repository.TemQuartosPorHotelId(id);

    }
}
