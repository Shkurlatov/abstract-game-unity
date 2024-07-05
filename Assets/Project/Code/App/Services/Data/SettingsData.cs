using System;
namespace App.Services.Progress
{
    [Serializable]
    public class SettingsData
    {
        public int GameMode;

        public SettingsData(int gameMode)
        {
            GameMode = gameMode;
        }
    }
}
