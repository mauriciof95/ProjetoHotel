using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Helpers;
using ProjetoHotel.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Controllers
{
    [Route("quarto")]
    public class QuartoController : Controller
    {
        private readonly QuartoServices _services;
        private IWebHostEnvironment _hostingEnvironment;

        public QuartoController(QuartoServices services, IWebHostEnvironment environment)
        {
            _services = services;
            _hostingEnvironment = environment;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _services.ListarTudo());
        }

        [HttpGet("cadastrar")]
        public async Task<IActionResult> Create()
        {
            ViewData["Hoteis"] = new SelectList(await _services.RetornarHoteisSelect(), "Id", "Descricao");
            return View();
        }


        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quarto quarto)
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
                await _services.Cadastrar(quarto, imagem_names);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hoteis"] = new SelectList(await _services.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
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

            ViewData["Hoteis"] = new SelectList(await _services.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
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
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Hoteis"] = new SelectList(await _services.RetornarHoteisSelect(), "Id", "Descricao", quarto.Hotel_Id);
                return View(quarto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpPost("deletar/{id:long}")]
        public async Task<IActionResult> Deletar(long id)
        {
            await _services.Deletar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
