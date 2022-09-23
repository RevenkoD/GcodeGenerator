using GCodeGeneratorReact.Dtos;
using GCodeGeneratorReact.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GCodeGeneratorReact.Controllers
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

		[HttpGet]
		public async Task<SettingsDto> Get()
		{
			return await _settingService.GetCurrentSettings();
		}

		[HttpPost]
		public async Task<SettingsDto> Save(SettingsDto dto)
		{
			return await _settingService.SetSettings(dto);
		}
	}
}
