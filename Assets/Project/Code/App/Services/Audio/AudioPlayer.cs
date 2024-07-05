using UnityEngine;

namespace App.Services.Audio
{
    public class AudioPlayer : MonoBehaviour, IAppAudio
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _effectSource;

        [SerializeField] private AudioClip _cardFlipClip;
        [SerializeField] private AudioClip _matchClip;
        [SerializeField] private AudioClip _mismatchClip;
        [SerializeField] private AudioClip _gameCompleteClip;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PlayCardFlipSound()
        {
            _soundSource.PlayOneShot(_cardFlipClip);
        }

        public void PlayMatchSound()
        {
            _effectSource.PlayOneShot(_matchClip);
        }

        public void PlayMismatchSound()
        {
            _effectSource.PlayOneShot(_mismatchClip);
        }

        public void PlayGameCompleteSound()
        {
            _effectSource.PlayOneShot(_gameCompleteClip);
        }

        public void Cleanup() { }
    }
}
