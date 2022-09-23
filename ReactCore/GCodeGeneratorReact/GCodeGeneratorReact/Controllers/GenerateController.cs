using ElectronNET.API;
using ElectronNET.API.Entities;
using GCodeGeneratorReact.Dtos;
using GCodeGeneratorReact.Services;
using Microsoft.AspNetCore.Mvc;

namespace GCodeGeneratorReact.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GenerateController : ControllerBase
	{
		private readonly GCodeService gCodeService;

		public GenerateController(GCodeService gCodeService)
		{
			this.gCodeService = gCodeService;
		}

		[HttpPost]
		public async Task<int> Generate(GeneratorRequestDto dto)
		{
			var options = new SaveDialogOptions
			{
				Title = "Save G-code",
				Filters = new FileFilter[]{
				new FileFilter { Name = "G-codes", Extensions = new string[] {"gcode", "txt"} }
				}
			};

			try
			{
				var mainWindow = Electron.WindowManager.BrowserWindows.First();
				var result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
				if (result == null)
					return 0;
				await gCodeService.GenerateGcode(dto, result);
				return 1;
			}
			catch
			{
				return 0;
			}
		}
	}
}
