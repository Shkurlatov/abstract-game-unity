using App.Services.Assets;
using App.Services.Audio;
using App.Services.Randomizer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cards
{
    public class CardManager : ICards
    {
        private readonly object _lock = new object(); 
        private readonly CardFactory _cardFactory;
        private readonly IAppRandomizer _randomizer;
        private readonly IAppAudio _audio;

        private List<Card> _cards = new List<Card>();
        private ConcurrentDictionary<int, bool> _blockedCardIds;
        private Card _waitingCompareCard;

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
            _blockedCardIds = new ConcurrentDictionary<int, bool>(Environment.ProcessorCount, _cards.Count);
            CompareResultAction = compareResultAction;

            ShuffleCards();

            CardPlacer cardPlacer = new CardPlacer(_cards, gameMode);
            cardPlacer.PlaceCards();

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

        private void ActivateCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Activate(i, OnCardClick, OnCardRelease);
            }
        }

        private void OnCardClick(Card card)
        {
            if (_blockedCardIds.TryAdd(card.Id, true))
            {
                CompareCard(card);
            }
        }

        private void OnCardRelease(int cardId)
        {
            if (_blockedCardIds.TryRemove(cardId, out _) == false)
            {
                Debug.LogError($"Card for id {cardId} lost on release operation.");
            }
        }

        private void CompareCard(Card card)
        {
            card.Open();

            lock (_lock)
            {
                if (_waitingCompareCard == null)
                {
                    _waitingCompareCard = card;
                }
                else
                {
                    PairHolder pairHolder = new PairHolder(CompareResultAction, _waitingCompareCard, card);
                    _waitingCompareCard = null;
                }
            }

            _audio.PlayCardFlipSound();
        }

        public void Cleanup()
        {
            _cards.Clear();
            _blockedCardIds.Clear();
            _waitingCompareCard = null;
        }
    }
}
