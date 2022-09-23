namespace GCodeGeneratorReact.Dtos
{
	public class SettingsDto
	{
		public string PreScript { get; set; }

		public string PostScript { get; set; }

		public double FullRoundSteps { get; set; }

		public double StepsPermm { get; set; }

		public bool XInversion { get; set; }

		public bool YInversion { get; set; }
	}
}
