using ElectronNET.API;
using ElectronNET.API.Entities;
using GCodeGeneratorReact.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GCodeGeneratorReact.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GenerateController : ControllerBase
	{
		[HttpPost]
		public async Task<int> Generate(GeneraterRequestDto dto)
		{
			var options = new SaveDialogOptions
			{
				Title = "Save G-code",
				Filters = new FileFilter[]{
				new FileFilter { Name = "G-codes", Extensions = new string[] {"gcode", "txt"} }
				}
			};
			var mainWindow = Electron.WindowManager.BrowserWindows.First();
			var result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
			return 1;
		}
	}
}
