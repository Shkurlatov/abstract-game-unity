using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private readonly float _rotationSpeed = 250.0f;
        private readonly float _demoDuration = 1.0f;
        private readonly float _fadeDuration = 1.0f;

        public int Id { get; private set; }
        public int TypeId { get; private set; }

        public event Action CardOpenedAction;

        private Action<Card> CardClickAction;
        private Action<int> CardReleaseAction;

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
            _button.interactable = false;
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

        public void Activate(int cardId, Action<Card> cardClickAction, Action<int> cardReleaseAction)
        {
            Id = cardId;
            CardClickAction = cardClickAction;
            CardReleaseAction = cardReleaseAction;
            StartCoroutine(InitialCardDemonstration());
        }

        public void OnCardClick()
        {
            CardClickAction(this);
        }

        public void Open()
        {
            _button.interactable = false;
            StopAllCoroutines();
            StartCoroutine(OpenCard());
        }

        public void Close()
        {
            StopAllCoroutines();
            StartCoroutine(CloseCard());
            CardReleaseAction(Id);
            _button.interactable = true;
        }

        public void Disappear()
        {
            StartCoroutine(DisappearCard());
        }

        private IEnumerator InitialCardDemonstration()
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

            yield return new WaitForSeconds(_demoDuration);

            StartCoroutine(CloseCard());
            _button.interactable = true;
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

            CardOpenedAction?.Invoke();
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
        }

        private IEnumerator DisappearCard()
        {
            Color imageColor = _image.color;
            float elapsedTime = 0.0f;

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(imageColor.a, 0.0f, elapsedTime / _fadeDuration);
                _image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
                yield return null;
            }
        }
    }
}
