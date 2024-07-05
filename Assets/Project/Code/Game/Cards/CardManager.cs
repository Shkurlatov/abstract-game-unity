using App.Services.Assets;
using App.Services.Audio;
using App.Services.Randomizer;
using System;
using System.Collections.Generic;

namespace Game.Cards
{
    public class CardManager : ICards
    {
        private readonly CardFactory _cardFactory;
        private readonly IAppRandomizer _randomizer;
        private readonly IAppAudio _audio;

        private List<Card> _cards = new List<Card>();
        private Queue<PairHolder> _pairPool = new Queue<PairHolder>();

        private Action<bool> CompareResultAction;

        public CardManager(IAppAssetProvider assets, IAppRandomizer randomizer, IAppAudio audio)
        {
            _cardFactory = new CardFactory(assets);
            _randomizer = randomizer;
            _audio = audio;
        }

        public void LayOut(GameMode gameMode, Action<bool> compareResultAction)
        {
            _cards = _cardFactory.CreateCards(gameMode.PairsCount);
            CompareResultAction = compareResultAction;

            ShuffleCards();

            CardPlacer cardPlacer = new CardPlacer(_cards, gameMode);
            cardPlacer.PlaceCards();

            FillPairPool();
            ActivateCards();
        }

        private void ShuffleCards()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int randomIndex = _randomizer.Randomize(0, i);
                (_cards[randomIndex], _cards[i]) = (_cards[i], _cards[randomIndex]);
            }
        }

        private void FillPairPool()
        {
            for (int i = 0; i < 10; i++)
            {
                _pairPool.Enqueue(new PairHolder(_audio, CompareResultAction));
            }
        }

        private void ActivateCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Activate(i, OnCardClick);
            }
        }

        private void OnCardClick(Card card)
        {
            AddCardToPairHolder(card);
        }

        private void AddCardToPairHolder(Card card)
        {
            foreach (PairHolder holder in _pairPool)
            {
                if (holder.IsAvailable)
                {
                    holder.AddCard(card);
                    return;
                }
            }

            PairHolder pairHolder = new PairHolder(_audio, CompareResultAction);
            pairHolder.AddCard(card);
            _pairPool.Enqueue(pairHolder);
        }

        public void Cleanup() { }
    }
}
