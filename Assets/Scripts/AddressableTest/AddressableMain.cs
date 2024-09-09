using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableMain : MonoBehaviour
{
    // public AssetReference _AssetReference;
    private void Start()
    {
        Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Pipes.prefab").Completed += (o) =>
        {
            GameObject prefab = o.Result;
            GameObject cube = Instantiate(prefab);
        };
        // _AssetReference.LoadAssetAsync<GameObject>().Completed += (o) =>
        // {
        //     GameObject prefab = o.Result;
        //     GameObject cube = Instantiate(prefab);
        // };
        AssetLabelReference textureLabel = null;
        Addressables.LoadAssetsAsync<Texture2D>(textureLabel, (texture) =>
            {
                // 没加载完一个资源，就回调一次
                Debug.Log("加载了一个资源： " + texture.name);
            });
            
    }
}