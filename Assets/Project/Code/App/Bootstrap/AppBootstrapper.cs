using UnityEngine;

namespace App.Bootstrap
{
    public class AppBootstrapper : MonoBehaviour
    {
        [SerializeField] SceneLoader _sceneLoader;

        private App _app;

        private void Awake()
        {
            _app = new App(_sceneLoader);

            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit() => 
            _app.OnApplicationQuit();
    }
}
