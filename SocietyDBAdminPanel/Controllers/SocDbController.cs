using Microsoft.AspNetCore.Mvc;
using SocietyDBAdminPanel.Core.Models;
using SocietyDBAdminPanel.Core.Services.SocietyDbService;
using System.Text.Json;

namespace SocietyDBAdminPanel.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SocDbController : Controller
    {
        private readonly ISocietyDbService _societyDbService;
        public SocDbController(ISocietyDbService societyDbService)
        {
            _societyDbService = societyDbService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _societyDbService.GetAllConnectionStringsAsync();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetSocDbById([FromQuery]int Id)
        {
            var result = await _societyDbService.GetSocDbByIdAsync(Id);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateSocDbById(AddUpdateSocDBMstModel model)
        {
            var result = await _societyDbService.AddOrUpdateSocDbMst(model);
            return Json(result);
        }
    }
}
