using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models.Exceptions;
using ProjetoHotel.Domain.Models.Request;
using ProjetoHotel.Helpers;
using ProjetoHotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Controllers
{
    [Route("hotel")]
    public class HotelController : Controller
    {

        private readonly HotelServices _services;

        public HotelController(HotelServices services)
            => _services = services;
        

        public async Task<IActionResult> Index()
        {
            return View(await _services.ListarTudo());
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
                    await _services.Cadastrar(hotel, imagem_names);
                    TempData["SuccessMessage"] = "Cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["WarningMessage"] = "Verifique se os campos estão preenchidos corretamente.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Não foi possivel cadastrar"; 
            }
            return View(hotel);
        }


        [HttpGet("editar/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await _services.BuscarPorId(id);
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
                    hotel = await _services.Editar(hotel, id);
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
                await _services.Deletar(id);

                TempData["SuccessMessage"] = "Deletado com sucesso!";
            }
            catch (ValidationException ex)
            {
                TempData["WarningMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao deletar.";
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("{hotel_id:long}/galeria")]
        public async Task<IActionResult> Gallery(long hotel_id)
        {
            var view = await _services.RetornarParaGaleria(hotel_id);
            if (view == null) return NotFound();

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
                    await _services.CadastrarImagem(file, hotel_id);
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
                await _services.DeletarImagem(hotel_id, id);
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
