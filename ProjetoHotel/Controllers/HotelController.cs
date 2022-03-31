using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Services;

namespace ProjetoHotel.Controllers
{
    [Route("hotel")]
    public class HotelController : Controller
    {

        private readonly HotelServices _services;

        public HotelController(HotelServices services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _services.ListarTudo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
            if (ModelState.IsValid)
            {
                await _services.Cadastrar(hotel);
                return RedirectToAction(nameof(Index));
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

        [HttpPut("{id:long}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel obj, long id)
        {
            try
            {
                if (id != obj.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    obj = await _services.Editar(obj, id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception) 
            {
                throw;
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _services.Deletar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
