using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void UpdateScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
