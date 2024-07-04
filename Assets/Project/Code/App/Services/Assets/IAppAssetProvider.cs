using Game.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace App.Services.Assets
{
    public interface IAppAssetProvider : IAppService
    {
        List<Card> GetCardVariants();
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
    }
}
