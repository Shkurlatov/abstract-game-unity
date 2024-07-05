using System;
using UnityEngine;

namespace Game.Cards
{
    public class PairHolder
    {
        private Card _initialCard;
        private Card _followCard;

        public bool IsAvailable => _followCard == null;

        private readonly Action<bool> CompareResultAction;

        public PairHolder(Action<bool> compareResultAction)
        {
            CompareResultAction = compareResultAction;
        }

        public void AddCard(Card card)
        {
            if (_initialCard == null)
            {
                _initialCard = card;
                _initialCard.Open();
                return;
            }

            if (_followCard == null)
            {
                _followCard = card;
                _followCard.CardOpenedAction += OnCardsOpened;
                _followCard.Open();
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

            CompareResultAction(isMatch);
        }
    }
}
