using UnityEngine;

namespace App.Bootstrap
{
    public class AppBootstrapper : MonoBehaviour
    {
        private App _app;

        private void Awake()
        {
            _app = new App();

            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit()
        {
            _app.OnApplicationQuit();
        }
    }
}
