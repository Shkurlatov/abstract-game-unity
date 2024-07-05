using UnityEngine;

namespace Game.Cards
{
    public class PairHolder
    {
        private Card _initialCard;
        private Card _followCard;

        public bool IsAvailable => _followCard == null;

        public void AddCard(Card card)
        {
            if (_initialCard == null)
            {
                _initialCard = card;
                card.Open();
                return;
            }

            if (_followCard == null)
            {
                _followCard = card;
                card.CardOpenedAction += OnCardsOpened;
                card.Open();
                return;
            }

            Debug.LogError($"Attempt to add card to busy pair holder.");
        }

        private void OnCardsOpened()
        {
            _followCard.CardOpenedAction -= OnCardsOpened;

            bool isMatch = _initialCard.TypeId == _followCard.TypeId;
            if (isMatch)
            {
                _initialCard.Disappear();
                _followCard.Disappear();
            }
            else
            {
                _initialCard.Close();
                _followCard.Close();
            }

            _initialCard = null;
            _followCard = null;
        }
    }
}
