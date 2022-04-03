using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models.Request;
using ProjetoHotel.Helpers;
using ProjetoHotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Controllers
{
    [Route("quarto")]
    public class QuartoController : Controller
    {
        private readonly QuartoServices _services;
        private HotelServices _hotelBusiness;

        public QuartoController(
            QuartoServices services,
            HotelServices hotelServices)
        {
            _services = services;
            _hotelBusiness = hotelServices;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _services.ListarTudo());
        }

        [HttpGet("cadastrar")]
        public async Task<IActionResult> Create()
        {
            ViewData["Hoteis"] = new SelectList(await _hotelBusiness.RetornarHoteisSelect(), "Id", "Descricao");
            return View();
        }


        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quarto quarto)
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
                    await _services.Cadastrar(quarto, imagem_names);
                    TempData["SuccessMessage"] = "Cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Hoteis"] = new SelectList(await _hotelBusiness.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
                TempData["WarningMessage"] = "Verifique se os campos estão preenchidos corretamente.";
            }
            catch (Exception)
            {
                ViewData["Hoteis"] = new SelectList(await _hotelBusiness.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
                TempData["ErrorMessage"] = "Não foi possivel cadastrar";
            }
            return View(quarto);
        }

        [HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var quarto = await _services.BuscarPorId(id);
            if (quarto == null)
            {
                return NotFound();
            }

            ViewData["Hoteis"] = new SelectList(await _hotelBusiness.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
            return View(quarto);
        }

        [HttpPost("editar/{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Quarto quarto, long id)
        {
            try
            {
                if (id != quarto.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    quarto = await _services.Editar(quarto, id);
                    TempData["SuccessMessage"] = "Editado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Hoteis"] = new SelectList(await _hotelBusiness.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
                TempData["WarningMessage"] = "Verifique se os campos estão preenchidos corretamente.";
                return View(quarto);
            }
            catch (Exception)
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
                await _services.Deletar(id);
                TempData["SuccessMessage"] = "Deletado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao deletar.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{quarto_id:long}/galeria")]
        public async Task<IActionResult> Gallery(long quarto_id)
        {
            var view = await _services.RetornarParaGaleria(quarto_id);
            if(view == null)
            {
                return NotFound();
            }

            ViewBag.Imagens = view.Imagens;
            ViewBag.Quarto = view.Nome_Quarto;
            ViewBag.Quarto_Id = quarto_id;
            return View();
        }

        [HttpPost("{quarto_id:long}/galeria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gallery(ImagemRequest file, long quarto_id)
        {
            try
            {
                if (file.Imagem != null)
                {
                    await _services.CadastrarImagem(file, quarto_id);
                }
                TempData["SuccessMessage"] = "Cadastrado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao cadastrar.";
            }
            return RedirectToAction(nameof(Gallery), new { quarto_id = quarto_id });
        }

        [HttpPost("{quarto_id:long}/galeria/deletar/{id:long}")]
        public async Task<IActionResult> DeletarImagem(long quarto_id, long id)
        {

            try
            {
                await _services.DeletarImagem(quarto_id, id);
                TempData["SuccessMessage"] = "Deletado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao deletar.";
            }

            return RedirectToAction(nameof(Gallery), new { quarto_id = quarto_id });
        }
    }
}
