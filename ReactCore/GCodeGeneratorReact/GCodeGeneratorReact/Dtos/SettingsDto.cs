namespace GCodeGeneratorReact.Dtos
{
	public class SettingsDto
	{
		public string PreScript { get; set; } = "G21\n";

		public string PostScript { get; set; } = "";

		public double FullRoundSteps { get; set; } = 126.942;

		public double StepsPermm { get; set; } = 1;

		public bool XInversion { get; set; } = false;

		public bool YInversion { get; set; } = false;
	}
}
