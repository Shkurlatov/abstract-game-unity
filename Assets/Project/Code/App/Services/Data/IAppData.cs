using System.Threading.Tasks;

namespace App.Services.Progress

{
    public interface IAppData : IAppService
    {
        Task SaveSettingsAsync(SettingsData settingsData);
        Task SaveProgressAsync(ProgressData progressData);
        Task<SettingsData> LoadSettingsAsync();
        Task<ProgressData> LoadProgressAsync();
    }
}
