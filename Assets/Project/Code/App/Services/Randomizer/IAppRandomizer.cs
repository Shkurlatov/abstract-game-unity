namespace App.Services.Randomizer
{
    public interface IAppRandomizer : IAppService
    {
        int Randomize(int minValue, int maxValue);
    }
}
