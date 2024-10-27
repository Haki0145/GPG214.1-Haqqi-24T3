using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetBundle : MonoBehaviour
{
    [SerializeField] string folderPath = "AssetBundles";
    [SerializeField] string fileName = "assetbundle.File";
    [SerializeField] string combinedPath;
    private UnityEngine.AssetBundle assetBundle;
    [SerializeField] GameObject groundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadAssetBundle();
        LoadGround();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadGround()
    {
        if(assetBundle == null)
        {
            return;
        }

        groundPrefab = assetBundle.LoadAsset<GameObject>("ground");

        if(groundPrefab != null)
        {
            var spawn = Instantiate(groundPrefab);
            spawn.transform.position += Vector3.up * 3;
        }

       
    }

    void LoadAssetBundle()
    {
        combinedPath = Path.Combine(folderPath, fileName);

        if (File.Exists(combinedPath))
        {
            assetBundle = UnityEngine.AssetBundle.LoadFromFile(combinedPath);
        }
        else
        {
            Debug.Log("file does not exists" + combinedPath);
        }
    }
}
