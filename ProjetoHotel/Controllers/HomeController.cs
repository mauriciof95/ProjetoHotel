using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjetoHotel.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }
      
        public async Task<IActionResult> Index() => View();
        
    }
}
