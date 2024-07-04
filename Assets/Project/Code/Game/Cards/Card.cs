using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Cards
{
    public class Card : MonoBehaviour
    {
        private enum State
        {
            None = 0,
            Ready = 1,
            Open = 2,
            Wait = 3,
            Match = 4,
            UnMatch = 5,
            Close = 6,
            Destroy = 7,
        }

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _button;

        private readonly float _rotationSpeed = 250.0f;

        private State _state;
        private Card _comparisonCard;

        public int TypeId { get; private set; }

        private Action<Card> CardClickAction;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnCardClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Initialize(int typeId)
        {
            TypeId = typeId;
        }

        public void SetSize(float cardSize)
        {
            _rectTransform.sizeDelta = new Vector2(cardSize, cardSize);
        }

        public void SetPosition(Vector2 cardPosition)
        {
            _rectTransform.localPosition = cardPosition;
        }

        public void Activate(Action<Card> onCardClickAction)
        {
            CardClickAction = onCardClickAction;
            _state = State.Ready;
        }

        public void ProcessMatchingResult(bool isMatch, Card comparisonCard)
        {
            _comparisonCard = comparisonCard;
            _comparisonCard.ApplyMatchingResult(isMatch);
            ApplyMatchingResult(isMatch);
        }

        private void OnCardClick()
        {
            if (_state != State.Ready)
            {
                return;
            }

            _state = State.Open;
            CardClickAction?.Invoke(this);
            StartCoroutine(OpenCard());
        }

        public void ApplyMatchingResult(bool isMatch)
        {
            switch (_state)
            {
                case State.Open:
                    _state = isMatch
                        ? State.Match
                        : State.UnMatch;
                    break;

                case State.Wait:
                    _state = isMatch
                        ? State.Destroy
                        : State.Close;
                    StartCoroutine(CloseCard());
                    break;

                default:
                    Debug.LogError($"Unexpected card state {_state} on set match result operation.");
                    break;
            }
        }

        private IEnumerator OpenCard()
        {
            float currentAngle = 0.0f;
            while (currentAngle < 180.0f)
            {
                currentAngle += _rotationSpeed * Time.deltaTime;

                if (currentAngle > 180.0f)
                {
                    currentAngle = 180.0f;
                }

                transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
                yield return null;
            }

            OnOpened();
        }
        
        private IEnumerator CloseCard()
        {
            float currentAngle = 180.0f;
            while (currentAngle > 0.0f)
            {
                currentAngle -= _rotationSpeed * Time.deltaTime;

                if (currentAngle < 0.0f)
                {
                    currentAngle = 0.0f;
                }

                transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
                yield return null;
            }

            OnClosed();
        }

        private void OnOpened()
        {
            switch (_state)
            {
                case State.Open:
                    _state = State.Wait;
                    break;

                case State.Match:
                    _state = State.Destroy;
                    StartCoroutine(CloseCard());
                    break;

                case State.UnMatch:
                    _state = State.Close;
                    StartCoroutine(CloseCard());
                    break;

                default:
                    Debug.LogError($"Unexpected card state {_state} on card opened operation.");
                    break;
            }
        }

        private void OnClosed()
        {
            if (_comparisonCard != null)
            {
                _comparisonCard = null;
            }

            switch (_state)
            {
                case State.Close:
                    _state = State.Ready;
                    break;

                case State.Destroy:
                    Destroy(gameObject);
                    break;

                default:
                    Debug.LogError($"Unexpected card state {_state} on card closed operation.");
                    break;
            }
        }
    }
}
