using App.Services;
using App.Services.Assets;
using App.Services.Audio;
using App.Services.Progress;
using Game.Cards;
using UI;
using UnityEngine;

namespace Game
{
    public class GameContext
    {
        public readonly IAppAssetProvider Assets;
        public readonly IAppData Data;
        public readonly IAppAudio Audio;

        public readonly ICards Cards;
        public readonly HomeButton HomeButton;
        public readonly Transform UIRoot;
        public readonly ScoreCounter ScoreCounter;
        public readonly GameMode GameMode;
        public readonly int Score;

        public GameContext(
            AppServiceContainer appContext,
            ICards cards,
            HomeButton homeButton,
            Transform uIRoot,
            ScoreCounter scoreCounter,
            GameMode gameMode,
            int score)
        {
            Assets = appContext.Single<IAppAssetProvider>();
            Data = appContext.Single<IAppData>();
            Audio = appContext.Single<IAppAudio>();

            Cards = cards;
            HomeButton = homeButton;
            UIRoot = uIRoot;
            ScoreCounter = scoreCounter;
            GameMode = gameMode;
            Score = score;
        }
    }
}
