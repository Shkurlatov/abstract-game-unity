using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Bootstrap
{
    public class SceneLoader
    {
        public async Task LoadSceneAsync(string sceneName, Action onLoaded)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            await LoadAsyncOperation(asyncOperation);

            asyncOperation.allowSceneActivation = true;
            onLoaded?.Invoke();
        }

        private Task LoadAsyncOperation(AsyncOperation asyncOperation)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            asyncOperation.completed += (AsyncOperation op) => tcs.SetResult(true);
            return tcs.Task;
        }
    }
}
