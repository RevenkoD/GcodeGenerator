using GCodeGeneratorReact.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GCodeGeneratorReact.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		[HttpGet]
		public SettingsDto Get()
		{
			return new SettingsDto { YInversion = true };
		}
	}
}
