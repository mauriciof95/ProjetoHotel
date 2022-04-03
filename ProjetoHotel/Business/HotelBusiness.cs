using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models;
using ProjetoHotel.Domain.Models.Exceptions;
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
    public class HotelBusiness
    {
        private HotelRepository _repository;
        private HotelImagemRepository _hotelImagemRepository;

        private QuartoBusiness _quartoServices;

        public HotelBusiness(SqlDbContext context)
        {
            _repository = new HotelRepository(context);
            _hotelImagemRepository = new HotelImagemRepository(context);
            _quartoServices = new QuartoBusiness(context);
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

            var Imagens = await _hotelImagemRepository.RetornarPorHotelId(id);

            if (Imagens.Count() > 0)
            {
                var imagemHelper = new ImagemHelper();
                foreach (var img in Imagens)
                {
                    imagemHelper.DeletarImagemDoDiretorio(img.Image_Url);
                    await _hotelImagemRepository.Deletar(img);
                }
            }
            await _repository.Deletar(id);
        }

        public async Task<GalleryHotelViewModel> RetornarParaGaleria(long hotel_id)
        {
            var imagemHelper = new ImagemHelper();
            GalleryHotelViewModel viewModel = new GalleryHotelViewModel();
            Hotel hotel = await BuscarPorId(hotel_id);
            viewModel.Imagens = await _hotelImagemRepository.RetornarPorHotelId(hotel_id);
            viewModel.Nome_Hotel = hotel.Nome;
            return viewModel;
        }

        public async Task CadastrarImagem(ImagemRequest file, long hotel_id)
        {
            var processarImagem = new ImagemHelper();
            string imagemName = processarImagem.SalvarImagem(file.Imagem);

            await _hotelImagemRepository.Cadastrar(new HotelImagem { 
                Hotel_Id = hotel_id,
                Image_Url = imagemName
            });
        }

        public async Task DeletarImagem(long hotel_id, long id)
        {
            var imagem = await _hotelImagemRepository.BuscarPorIdHotelId(id, hotel_id);
            var processarImagem = new ImagemHelper();
            processarImagem.DeletarImagemDoDiretorio(imagem.Image_Url);
            await _hotelImagemRepository.Deletar(imagem);
        }
    }
}
