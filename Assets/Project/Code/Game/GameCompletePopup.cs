using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class GameCompletePopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _popupText;

        private readonly float _duration = 2.0f;
        private readonly float _fadeSpeed = 1.0f;
        private readonly float _growAmount = 1.2f;
        private readonly float _moveSpeed = 50.0f;

        private float _elapsedTime = 0.0f;
        private Color _originalColor;

        private Action CompleteAction;

        private void Awake()
        {
            _originalColor = _popupText.color;
            SetTextAlpha(0);
        }

        public void Initialize(Action completeAction)
        {
            CompleteAction = completeAction;
            StartCoroutine(AnimatePopup());
        }

        private IEnumerator AnimatePopup()
        {
            Vector3 originalScale = _popupText.transform.localScale;
            Vector3 targetScale = originalScale * _growAmount;
            Vector3 originalPosition = _popupText.transform.localPosition;

            while (_elapsedTime < _duration)
            {
                _elapsedTime += Time.deltaTime;
                float time = _elapsedTime / _duration;

                AnimateFadeIn(time);
                AnimateScale(originalScale, targetScale, time);
                AnimateMove(originalPosition, time);

                yield return null;
            }

            CompleteAnimation(targetScale, originalPosition);

            yield return new WaitForSeconds(_duration);

            CompleteAction?.Invoke();
            Destroy(gameObject);
        }

        private void AnimateFadeIn(float time)
        {
            Color newColor = _originalColor;
            newColor.a = Mathf.Lerp(_originalColor.a, 1.0f, time * _fadeSpeed);
            _popupText.color = newColor;
        }

        private void AnimateScale(Vector3 originalScale, Vector3 targetScale, float time)
        {
            _popupText.transform.localScale = Vector3.Lerp(originalScale, targetScale, time);
        }

        private void AnimateMove(Vector3 originalPosition, float time)
        {
            _popupText.transform.localPosition = new Vector3(
                originalPosition.x,
                originalPosition.y + (time * _moveSpeed),
                originalPosition.z
            );
        }

        private void CompleteAnimation(Vector3 targetScale, Vector3 originalPosition)
        {
            SetTextAlpha(1.0f);
            _popupText.transform.localScale = targetScale;
            _popupText.transform.localPosition = new Vector3(
                originalPosition.x,
                originalPosition.y + _moveSpeed,
                originalPosition.z
            );
        }

        private void SetTextAlpha(float alpha)
        {
            Color color = _popupText.color;
            color.a = alpha;
            _popupText.color = color;
        }
    }
}
