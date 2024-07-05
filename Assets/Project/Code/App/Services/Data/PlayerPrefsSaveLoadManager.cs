using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Services.Progress
{
    public class PlayerPrefsSaveLoadManager : IAppData
    {
        private const string SETTINGS_KEY = "SettingsData";
        private const string PROGRESS_KEY = "ProgressData";

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task SaveSettingsAsync(SettingsData settingsData)
        {
            await _semaphore.WaitAsync();
            try
            {
                string json = JsonUtility.ToJson(settingsData);
                PlayerPrefs.SetString(SETTINGS_KEY, json);
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save settings data. Exception: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task SaveProgressAsync(ProgressData progressData)
        {
            await _semaphore.WaitAsync();
            try
            {
                string json = JsonUtility.ToJson(progressData);
                PlayerPrefs.SetString(PROGRESS_KEY, json);
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save progress data. Exception: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<SettingsData> LoadSettingsAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (PlayerPrefs.HasKey(SETTINGS_KEY))
                {
                    string json = PlayerPrefs.GetString(SETTINGS_KEY);
                    SettingsData settingsData = JsonUtility.FromJson<SettingsData>(json);
                    return await Task.FromResult(settingsData);
                }
                return await Task.FromResult(new SettingsData(4));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load settings data. Exception: {ex.Message}");
                return await Task.FromResult(new SettingsData(4));
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<ProgressData> LoadProgressAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (PlayerPrefs.HasKey(PROGRESS_KEY))
                {
                    string json = PlayerPrefs.GetString(PROGRESS_KEY);
                    ProgressData progressData = JsonUtility.FromJson<ProgressData>(json);
                    return await Task.FromResult(progressData);
                }
                return await Task.FromResult(new ProgressData(0));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load progress data. Exception: {ex.Message}");
                return await Task.FromResult(new ProgressData(0));
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Cleanup()
        {
            _semaphore.Dispose();
        }
    }
}
