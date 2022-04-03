using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models;
using ProjetoHotel.Domain.Models.Request;
using ProjetoHotel.Domain.Models.ViewModel;
using ProjetoHotel.Helpers;
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
        private QuartoImagemRepository _quartoImagemRepository;

        public QuartoBusiness(SqlDbContext context)
        {
            _repository = new QuartoRepository(context);
            _quartoImagemRepository = new QuartoImagemRepository(context);
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
            var Imagens = await _quartoImagemRepository.RetornarPorQuartoId(id);

            if (Imagens.Count() > 0)
            {
                var imagemHelper = new ImagemHelper();
                foreach (var img in Imagens)
                {
                    imagemHelper.DeletarImagemDoDiretorio(img.Image_Url);
                    await _quartoImagemRepository.Deletar(img);
                }
            }
            await _repository.DeletarTudo(id);
        }

        public async Task<bool> TemQuartosPorHotelId(long id)
             => await _repository.TemQuartosPorHotelId(id);


        public async Task<GalleryQuartoViewModel> RetornarParaGaleria(long quarto_id)
        {
            var imagemHelper = new ImagemHelper();
            GalleryQuartoViewModel viewModel = new GalleryQuartoViewModel();
            Quarto quarto = await BuscarPorId(quarto_id);
            viewModel.Imagens = await _quartoImagemRepository.RetornarPorQuartoId(quarto_id);
            viewModel.Nome_Quarto = quarto.Nome;
            return viewModel;
        }

        public async Task CadastrarImagem(ImagemRequest file, long quarto_id)
        {
            var processarImagem = new ImagemHelper();
            string imagemName = processarImagem.SalvarImagem(file.Imagem);

            await _quartoImagemRepository.Cadastrar(new QuartoImagem
            {
                Quarto_Id = quarto_id,
                Image_Url = imagemName
            });
        }

        public async Task DeletarImagem(long hotel_id, long id)
        {
            var imagem = await _quartoImagemRepository.BuscarPorIdQuartoId(id, hotel_id);
            var processarImagem = new ImagemHelper();
            processarImagem.DeletarImagemDoDiretorio(imagem.Image_Url);
            await _quartoImagemRepository.Deletar(imagem);
        }

    }
}
