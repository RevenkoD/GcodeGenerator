using ElectronNET.API;
using ElectronNET.API.Entities;
using GCodeGeneratorReact.Dtos;
using GCodeGeneratorReact.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

		[HttpPost]
		[Route("preset")]
		public async Task<int> SavePreset(GeneratorRequestDto dto)
		{
			var options = new SaveDialogOptions
			{
				Title = "Save G-code",
				Filters = new FileFilter[]{
					new FileFilter { Name = "Preset", Extensions = new string[] {"gcode-preset"} }
				},
			};
			try
			{
				var mainWindow = Electron.WindowManager.BrowserWindows.First();
				var result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
				if (result == null)
					return 0;
				using var fs = new StreamWriter(result, false);
				await JsonSerializer.SerializeAsync(fs.BaseStream, dto);

				return 1;
			}
			catch
			{
				return 0;
			}
		}

		[HttpGet]
		[Route("preset")]
		public async Task<GeneratorRequestDto> OpenPreset()
		{
			var options = new OpenDialogOptions
			{
				Title = "Save preset",
				Filters = new FileFilter[]{
					new FileFilter { Name = "Preset", Extensions = new string[] {"gcode-preset"} }
				},
				Properties = new[] { OpenDialogProperty.openFile }
			};

			var mainWindow = Electron.WindowManager.BrowserWindows.First();
			var result = (await Electron.Dialog.ShowOpenDialogAsync(mainWindow, options))?.FirstOrDefault();

			if (string.IsNullOrWhiteSpace(result))
				throw new Exception("no file");
			if (!System.IO.File.Exists(result))
				throw new Exception("no file");

			using var fs = System.IO.File.OpenRead(result);
			var preset = JsonSerializer.Deserialize<GeneratorRequestDto>(fs);
			if (preset == null)
				throw new Exception("wrong file");
			return preset;
		}
	}
}
