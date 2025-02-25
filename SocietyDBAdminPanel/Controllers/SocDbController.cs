using Microsoft.AspNetCore.Mvc;

namespace SocietyDBAdminPanel.Controllers
{
    public class SocDbController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
