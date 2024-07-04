using UnityEngine;
using UnityEngine.UI;

namespace Game.Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _button;

        private bool isFlipped = false;

        public int TypeId {  get; private set; }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnCardClicked);
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

        public void OnCardClicked()
        {
            if (isFlipped)
                return;

            isFlipped = true;
            _rectTransform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        public void FlipBack()
        {
            isFlipped = false;
            _rectTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
}