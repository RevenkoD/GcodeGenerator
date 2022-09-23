using GCodeGeneratorReact.Dtos;

namespace GCodeGeneratorReact.Services
{
	public class GCodeService
	{
		private const string MandatoryPreScript = "G91\n";
		public async Task<int> GenerateGcode(GeneratorRequestDto dto, SettingsDto settings, string filePath)
		{
			var lineCount = 1;

			var s = Math.PI * dto.Diameter * Math.Tan((Math.PI / 180) * dto.Angle); // x move for one turn
			var rotation = dto.Length * settings.FullRoundSteps / s;

			var F_forMove = dto.SpeedOfFiber * Math.Sqrt(settings.FullRoundSteps * settings.FullRoundSteps + s * s)
				/ Math.Sqrt(Math.PI * dto.Diameter * Math.PI * dto.Diameter + s * s);

			var Fy = settings.FullRoundSteps * F_forMove / Math.Sqrt(settings.FullRoundSteps * settings.FullRoundSteps + s * s);

			var angleAddition = (2 * (dto.FiberWidth / dto.Diameter) * (180 / Math.PI));

			var fullRoundRotation = rotation * 2;

			var countOfLoops = fullRoundRotation / settings.FullRoundSteps;

			var addition = Math.Round(countOfLoops) - countOfLoops;


			await using var file = new StreamWriter(filePath, false);
			await file.WriteLineAsync(MandatoryPreScript);
			await file.WriteLineAsync(settings.PreScript);

			for (int i = 0; i < 10; i++)
			{
				await file.WriteLineAsync($"G01 X{dto.Length} Y-{rotation:.3f} F{F_forMove:.3f}");
				await file.WriteLineAsync($"G01 Y-{2 * settings.FullRoundSteps:.3f} F{Fy:.3f}");
				await file.WriteLineAsync($"G01 X-{dto.Length} Y-{rotation:.3f} F{F_forMove:.3f}");
				await file.WriteLineAsync($"G01 Y-{(((2 * 360 + addition * 360 + angleAddition) * settings.FullRoundSteps) / 360):.3f} F{Fy:.3f}");
			}

			await file.WriteLineAsync(settings.PostScript);
			return lineCount;
		}
	}
}
