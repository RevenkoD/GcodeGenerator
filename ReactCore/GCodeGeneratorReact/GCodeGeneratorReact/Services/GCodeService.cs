using GCodeGeneratorReact.Dtos;
using GCodeGeneratorReact.Interfaces;

namespace GCodeGeneratorReact.Services
{
	public class GCodeService
	{

		private const string MandatoryPreScript = "G91\n";
		private readonly ISettingService _settingService;

		public GCodeService(ISettingService settingService)
		{
			_settingService = settingService;
		}

		public async Task<int> GenerateGcode(GeneratorRequestDto dto, string filePath)
		{
			var lineCount = 1;
			var settings = await _settingService.GetCurrentSettings();

			var s = Math.PI * dto.Diameter * Math.Tan((Math.PI / 180) * dto.Angle); // x move for one turn
			var rotation = dto.Length * settings.FullRoundSteps / s;

			var F_forMove = dto.SpeedOfFiber * Math.Sqrt(settings.FullRoundSteps * settings.FullRoundSteps + s * s)
				/ Math.Sqrt(Math.PI * dto.Diameter * Math.PI * dto.Diameter + s * s);

			var Fy = settings.FullRoundSteps * F_forMove / Math.Sqrt(settings.FullRoundSteps * settings.FullRoundSteps + s * s);

			var pseudoFiberWidth = dto.FiberWidth / Math.Sin((Math.PI / 180) * dto.Angle);

			var angleAddition = (2 * (pseudoFiberWidth / dto.Diameter) * (180 / Math.PI));

			var fullRoundRotation = rotation * 2;

			var countOfLoops = fullRoundRotation / settings.FullRoundSteps;

			var addition = Math.Round(countOfLoops) - countOfLoops;

			var circleWithAdditional = ((((1 + dto.CountOfExtraLoop) + addition) * 360 + angleAddition) * settings.FullRoundSteps) / 360;
			var invX = settings.XInversion ? -1 : 1;
			var invY = settings.YInversion ? -1 : 1;


			await using var file = new StreamWriter(filePath, false);
			await file.WriteLineAsync(MandatoryPreScript);
			await file.WriteLineAsync(settings.PreScript);
			await file.WriteLineAsync($"G01 Y-{settings.FullRoundSteps * 2} F{Fy:f3}");

			var countOfRepeatForFull = Math.Round(360 / angleAddition + 0.5);

			var countOfRepeats = (dto.UseLoops == 1) ? dto.CountOfLoops : (countOfRepeatForFull * dto.CountOfFullLoops);

			for (int i = 0; i < countOfRepeats; i++)
			{
				await file.WriteLineAsync();
				await file.WriteLineAsync($"G01 X{invX * dto.Length} Y{invY * rotation:f3} F{F_forMove:f3}");
				await file.WriteLineAsync($"G01 Y{invY * (1 + dto.CountOfExtraLoop) * settings.FullRoundSteps:f3} F{Fy:f3}");
				await file.WriteLineAsync($"G01 X-{invX * dto.Length} Y{invY * rotation:f3} F{F_forMove:f3}");
				await file.WriteLineAsync($"G01 Y{invY * circleWithAdditional:f3} F{Fy:f3}");
				lineCount += 4;
			}

			await file.WriteLineAsync(settings.PostScript);
			return lineCount;
		}
	}
}
