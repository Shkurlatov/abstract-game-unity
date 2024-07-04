using UnityEngine;

namespace App.Bootstrap
{
    public class AppRunner : MonoBehaviour
    {
        public AppBootstrapper AppBootstrapperPrefab;

        private void Awake()
        {
            AppBootstrapper bootstrapper = FindObjectOfType<AppBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(AppBootstrapperPrefab);
            }
        }
    }
}
