using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask InitializeAsync();
        UniTask<GameObject> Instantiate(string path, Vector3 at);
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        UniTask<TAsset> Load<TAsset>(string key) where TAsset : class;
        UniTask<List<string>> GetAssetsListByLabel<TAsset>(string label);
        UniTask<List<string>> GetAssetsListByLabel(string label, Type type = null);
        UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        UniTask WarmupAssetsByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
        void Cleanup();
    }
}