using App.Services.Assets;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cards
{
    public class CardFactory
    {
        private readonly List<Card> _cardVariants;
        private readonly Transform _board;

        public CardFactory(IAppAssetProvider assets)
        {
            _cardVariants = assets.GetCardVariants();
            _board = assets.Instantiate(AssetPath.BOARD).transform;

            if (_cardVariants == null || _cardVariants.Count == 0)
            {
                Debug.LogError("No card variants available in the asset provider.");
            }
        }

        public List<Card> CreateCards(int pairsCount)
        {
            List<Card> allCards = new List<Card>(pairsCount * 2);

            for (int i = 0; i < pairsCount; i++)
            {
                allCards.Add(CreateCard(i));
                allCards.Add(CreateCard(i));
            }

            return allCards;
        }

        private Card CreateCard(int index)
        {
            if (index >= _cardVariants.Count)
            {
                index %= _cardVariants.Count;
            }

            Card card = Object.Instantiate(_cardVariants[index], _board);
            card.Initialize(typeId: index + 1);
            return card;
        }
    }
}
