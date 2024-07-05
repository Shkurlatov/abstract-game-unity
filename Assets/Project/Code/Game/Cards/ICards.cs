using System;

namespace Game.Cards
{
    public interface ICards
    {
        void LayOut(GameMode gameMode, Action<bool> matchResultAction);
        void Cleanup();
    }
}
