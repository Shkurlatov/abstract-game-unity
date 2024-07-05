using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Bootstrap
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(string sceneName, Action onLoaded = null) =>
            StartCoroutine(LoadScene(sceneName, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (waitNextScene.isDone == false)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}