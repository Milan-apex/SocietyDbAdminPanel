﻿using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var result = await _societyDbService.GetAllConnectionStringsAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred.",
                    error = ex.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSocDbById([FromQuery] int Id)
        {
            try
            {
                var result = await _societyDbService.GetSocDbByIdAsync(Id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred.",
                    error = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateSocDbById([FromBody] AddUpdateSocDBMstModel model)
        {
            try
            {
                var result = await _societyDbService.AddOrUpdateSocDbMst(model);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred.",
                    error = ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordById(int id)
        {
            try
            {
                bool isDeleted = await _societyDbService.DeleteRecordById(id);

                if (!isDeleted)
                    return Ok(new { message = "Record deleted successfully." });

                return NotFound(new { message = "Record not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSocInfoBySocCode([FromQuery] string SocCode)
        {
            try
            {
                var result = await _societyDbService.GetSocInfoByIdAsync(SocCode);
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred.",
                    error = ex.Message
                });
            }
        }
    }
}
