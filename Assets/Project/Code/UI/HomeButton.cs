using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;

        public event Action HomeAction;

        private void OnEnable()
        {
            _homeButton.onClick.AddListener(OnHomeButtonClick);
        }

        private void OnDisable()
        {
            _homeButton.onClick.RemoveAllListeners();
        }

        private void OnHomeButtonClick()
        {
            HomeAction?.Invoke();
        }
    }

}
