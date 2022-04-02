using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models.Exceptions;
using ProjetoHotel.Helpers;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Business;
using ProjetoHotel.Domain.Models.Request;

namespace ProjetoHotel.Controllers
{
    [Route("hotel")]
    public class HotelController : Controller
    {

        private readonly HotelBusiness _business;

        public HotelController(HotelBusiness services)
            => _business = services;
        

        public async Task<IActionResult> Index()
        {
            return View(await _business.ListarTudo());
        }

        [HttpGet("cadastrar")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            try
            {
                IFormFileCollection imagens = Request.Form.Files;
                List<string> imagem_names = null;

                if (ModelState.IsValid)
                {
                    if (imagens.Count() > 0)
                    {
                        var processarImagem = new ImagemHelper();
                        imagem_names = processarImagem.SalvarImagem(imagens);
                    }
                    await _business.Cadastrar(hotel, imagem_names);
                    TempData["SuccessMessage"] = "Cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["WarningMessage"] = "Verifique se os campos estão preenchidos corretamente.";
                return View(hotel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Não foi possivel cadastrar";
                return View(hotel);
            }
        }


        [HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await _business.BuscarPorId(id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost("editar/{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel hotel, long id)
        {
            try
            {
                if (id != hotel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    hotel = await _business.Editar(hotel, id);
                    TempData["SuccessMessage"] = "Editado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["WarningMessage"] = "Verifique se os campos estão preenchidos corretamente.";
                return View(hotel);
            }
            catch(Exception) 
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao Editar.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost("deletar/{id:long}")]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _business.Deletar(id);

                TempData["SuccessMessage"] = "Deletado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException ex)
            {
                TempData["WarningMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao deletar.";
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet("{hotel_id:long}/galeria")]
        public async Task<IActionResult> Gallery(long hotel_id)
        {
            var view = await _business.RetornarParaGaleria(hotel_id);
            ViewBag.Imagens = view.Imagens;
            ViewBag.Hotel = view.Nome_Hotel;
            ViewBag.Hotel_Id = hotel_id;
            return View();
        }

        [HttpPost("{hotel_id:long}/galeria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gallery(ImagemRequest file, long hotel_id)
        {
            try
            {
                if (file.Imagem != null)
                {
                    await _business.CadastrarImagem(file, hotel_id);
                }
                TempData["SuccessMessage"] = "Cadastrado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao cadastrar.";
            }
            return RedirectToAction(nameof(Gallery), new { hotel_id = hotel_id });
        }

        [HttpPost("{hotel_id:long}/galeria/deletar/{id:long}")]
        public async Task<IActionResult> DeletarImagem(long hotel_id, long id)
        {
            
            try
            {
                await _business.DeletarImagem(hotel_id, id);
                TempData["SuccessMessage"] = "Deletado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao deletar.";
            }

            return RedirectToAction(nameof(Gallery), new { hotel_id = hotel_id });
        }
    }
}
