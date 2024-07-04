using UnityEngine;

namespace App.Services.Assets
{
    public interface IAppAssetProvider : IAppService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
    }
}
