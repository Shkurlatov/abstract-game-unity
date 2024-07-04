using App.Services.Assets;
using App.Services.Randomizer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cards
{
    public class CardManager : ICards
    {
        private readonly CardFactory _cardFactory;
        private readonly IAppRandomizer _randomizer;

        private List<Card> _cards = new List<Card>();

        public CardManager(IAppAssetProvider assets, IAppRandomizer randomizer)
        {
            _cardFactory = new CardFactory(assets);
            _randomizer = randomizer;
        }

        public void LayOut(GameMode gameMode)
        {
            _cards = _cardFactory.CreateCards(gameMode.PairsCount);
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
            foreach (Card card in _cards)
            {
                card.Activate(OnCardClick);
            }
        }

        private void OnCardClick(Card card)
        {
            Debug.Log("OnCardClick");
        }
    }
}
