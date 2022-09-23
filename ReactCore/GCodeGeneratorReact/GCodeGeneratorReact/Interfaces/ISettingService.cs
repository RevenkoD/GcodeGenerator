using GCodeGeneratorReact.Dtos;

namespace GCodeGeneratorReact.Interfaces
{
	public interface ISettingService
	{
		Task<SettingsDto> GetCurrentSettings();

		Task<SettingsDto> SetSettings(SettingsDto settings);

		Task<SettingsDto> GetDefaultSettings();

		Task SetDefaultSettings();
	}
}
