using System;
namespace App.Services.Progress
{
    [Serializable]
    public class ProgressData
    {
        public int Score;

        public ProgressData(int score)
        {
            Score = score;
        }
    }
}
