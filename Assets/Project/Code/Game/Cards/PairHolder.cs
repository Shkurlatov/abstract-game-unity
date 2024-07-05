using App.Services.Audio;
using System;
using UnityEngine;

namespace Game.Cards
{
    public class PairHolder
    {
        private readonly IAppAudio _audio;

        private Card _initialCard;
        private Card _followCard;

        public bool IsAvailable => _followCard == null;

        private readonly Action<bool> CompareResultAction;

        public PairHolder(IAppAudio audio, Action<bool> compareResultAction)
        {
            _audio = audio;
            CompareResultAction = compareResultAction;
        }

        public void AddCard(Card card)
        {
            if (_initialCard == null)
            {
                _initialCard = card;
                _initialCard.Open();
                _audio.PlayCardFlipSound();
                return;
            }

            if (_followCard == null)
            {
                _followCard = card;
                _followCard.CardOpenedAction += OnCardsOpened;
                _followCard.Open();
                _audio.PlayCardFlipSound();
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
                _audio.PlayMatchSound();
            }
            else
            {
                _initialCard.Close();
                _followCard.Close();
                _audio.PlayMismatchSound();
            }

            _initialCard = null;
            _followCard = null;

            CompareResultAction(isMatch);
        }
    }
}
