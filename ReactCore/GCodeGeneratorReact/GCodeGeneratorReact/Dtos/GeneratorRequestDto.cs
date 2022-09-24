namespace GCodeGeneratorReact.Dtos
{
	public class GeneratorRequestDto
	{
		public double Length { get; set; }

		public double Diameter { get; set; }

		public double Angle { get; set; }

		public double SpeedOfFiber { get; set; }

		public double FiberWidth { get; set; }

		public double CountOfExtraLoop { get; set; }

		public double CountOfLoops { get; set; }

		public double CountOfFullLoops { get; set; }

		public int UseLoops { get; set; }
	}
}
