using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;

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
            Debug.Log("Home button clicked!");
        }
    }
}