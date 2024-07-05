using System;

namespace Game.Cards
{
    public class PairHolder
    {
        private Card _initialCard;
        private Card _followCard;

        public bool IsAvailable => _followCard == null;

        private Action<bool> CompareResultAction;

        public PairHolder(Action<bool> compareResultAction, Card initialCard, Card followCard)
        {
            CompareResultAction = compareResultAction;
            _initialCard = initialCard;
            _followCard = followCard;
            _followCard.CardOpenedAction += OnCardsOpened;
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
            CompareResultAction = null;
        }
    }
}
