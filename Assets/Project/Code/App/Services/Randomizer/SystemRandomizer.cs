using System;

namespace App.Services.Randomizer
{
    public class SystemRandomizer : IAppRandomizer
    {
        public int Randomize(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        public void Cleanup() { }
    }
}
