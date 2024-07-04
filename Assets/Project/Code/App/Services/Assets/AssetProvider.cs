using Game.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Services.Assets
{
    public class AssetProvider : IAppAssetProvider
    {
        private readonly Card[] _cardVariants;

        public AssetProvider()
        {
            _cardVariants = Resources.LoadAll<Card>(AssetPath.CARDS);
        }

        public List<Card> GetCardVariants()
        {
            return _cardVariants.ToList();
        }

        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }

        public void Cleanup() { }
    }
}
