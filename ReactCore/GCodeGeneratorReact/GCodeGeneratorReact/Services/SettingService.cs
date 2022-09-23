using GCodeGeneratorReact.Dtos;
using GCodeGeneratorReact.Interfaces;
using System.Text.Json;

namespace GCodeGeneratorReact.Services
{
	public class SettingService : ISettingService
	{
		private SettingsDto _settings;
		private readonly string _path = "\\settings.json";
		public SettingService()
		{
			if (File.Exists(_path))
			{
				using var fs = File.OpenRead(_path);
				_settings = JsonSerializer.Deserialize<SettingsDto>(fs) ?? new SettingsDto();
			}
			else
				_settings = new SettingsDto();

		}
		public async Task<SettingsDto> GetCurrentSettings()
		{
			return _settings;
		}

		public async Task<SettingsDto> GetDefaultSettings()
		{
			return new SettingsDto();
		}

		public async Task SetDefaultSettings()
		{
			_settings = new SettingsDto();
			await SaveSettings(_settings);
		}

		public async Task<SettingsDto> SetSettings(SettingsDto settings)
		{
			_settings = settings;
			await SaveSettings(_settings);
			return _settings;
		}

		private async Task SaveSettings(SettingsDto settings)
		{
			using var fs = new StreamWriter(_path, false);
			await JsonSerializer.SerializeAsync(fs.BaseStream, settings);
		}
	}
}
