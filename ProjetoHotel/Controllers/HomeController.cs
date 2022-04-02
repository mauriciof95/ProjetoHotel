using Microsoft.AspNetCore.Mvc;
using ProjetoHotel.Infrastructure.Context;
using System.Threading.Tasks;

namespace ProjetoHotel.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(SqlDbContext context) { }
      
        public async Task<IActionResult> Index()
        {
                return View();
        }
    }
}
