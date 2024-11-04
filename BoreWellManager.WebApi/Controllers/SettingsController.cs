using BoreWellManager.Business.Operations.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;
        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPatch]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ToggleMaintenence()
        {
            await _settingService.ToggleMaintenence();
            return Ok();
        }
    }
}
