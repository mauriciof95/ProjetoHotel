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
using ProjetoHotel.Helpers;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Services;

namespace ProjetoHotel.Controllers
{
    [Route("hotel")]
    public class HotelController : Controller
    {

        private readonly HotelServices _services;
        private IWebHostEnvironment _hostingEnvironment;

        public HotelController(HotelServices services, IWebHostEnvironment environment)
        {
            _services = services;
            _hostingEnvironment = environment;
        }

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
                        var processarImagem = new ProcessarImagem(_hostingEnvironment);
                        imagem_names = processarImagem.SalvarImagem(imagens);
                    }

                    await _services.Cadastrar(hotel, imagem_names);
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Aconteceu um problema ao deletar.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
