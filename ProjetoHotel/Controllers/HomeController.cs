using Microsoft.AspNetCore.Mvc;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Services;
using System;
using System.Threading.Tasks;

namespace ProjetoHotel.Controllers
{
    public class HomeController : Controller
    {
        private HotelServices _hotelServices;

        public HomeController(SqlDbContext context)
        {
            _hotelServices = new HotelServices(context);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var hoteis = await _hotelServices.ListarTudo();
                return View(hoteis);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
    }
}
